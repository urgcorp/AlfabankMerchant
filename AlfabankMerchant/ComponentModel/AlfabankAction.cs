using System.Collections;
using System.Reflection;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AlfabankMerchant.ComponentModel
{
    public abstract class AlfabankAction
    {
        public const string REQUEST_SEND_DATETIME_FORMAT = "yyyyMMddHHmmss";

        /// <summary>
        /// Override URL path for action
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual string? ActionUrl { get; set; }

        /// <summary>
        /// <para>Find URL path defined within action</para>
        /// <para>Returns <see cref="ActionUrl"/> if defined</para>
        /// </summary>
        /// <param name="clientType">Type of client which expects defined URL</param>
        /// <returns>URL defined in action</returns>
        public string? FindActionUrl(string clientType)
        {
            if (!string.IsNullOrEmpty(ActionUrl))
                return ActionUrl;
            if (string.IsNullOrEmpty(clientType))
                throw new ArgumentException(nameof(clientType));

            var actionAttribute = (ActionUrlAttribute?)Attribute.GetCustomAttribute(this.GetType(), typeof(ActionUrlAttribute));
            if (actionAttribute != null && actionAttribute.ClientType.Equals(clientType, StringComparison.OrdinalIgnoreCase))
                return actionAttribute.Url;
            
            return null;
        }

        [ActionProperty("userName", Type = "AN..30")]
        [JsonProperty("userName")]
        [JsonPropertyName("userName")]
        protected string? Login { get; set; }

        [ActionProperty("password", Type = "AN..30")]
        [JsonProperty("password")]
        [JsonPropertyName("password")]
        protected string? Password { get; set; }

        /// <summary>
        /// Открытый ключ, который можно использовать для регистрации заказа.
        /// <para>Если для аутентификации при регистрации заказа используются логин и пароль, параметр token передавать не нужно</para>
        /// </summary>
        [ActionProperty("token", Type = "AN..30")]
        [JsonProperty("token")]
        [JsonPropertyName("token")]
        protected string? Token { get; set; }

        [Newtonsoft.Json.JsonExtensionData]
        [System.Text.Json.Serialization.JsonExtensionData]
        public IDictionary<string, object?> Extensions { get; set; } = new Dictionary<string, object?>();

        protected AlfabankAction()
        { }

        protected AlfabankAction(string? login, string? password, string? token)
        {
            Login = login;
            Password = password;
            Token = token;
        }
        
        /// <summary>Extract query parameters from action</summary>
        /// <returns>Set of parameters to send with HTTP request</returns>
        public Dictionary<string, string?> GetActionParams()
        {
            var queryParams = new Dictionary<string, string?>();

            foreach (var kvp in Extensions)
            {
                if (kvp.Value == null)
                    queryParams.Add(kvp.Key, null);
                else if (kvp.Value is string strVal)
                    queryParams.Add(kvp.Key, strVal);
                else
                    queryParams.Add(kvp.Key, kvp.Value.ToString());
            }
            
            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var property in properties)
            {
                var propAttr = property.GetCustomAttribute<ActionPropertyAttribute>();
                if (propAttr != null)
                {
                    var value = property.GetValue(this);
                    if (value != null)
                    {
                        if (value is string valueStr)
                            queryParams[propAttr.Name] = valueStr;
                        else
                            queryParams[propAttr.Name] = value.ToString();
                    }
                }
            }

            return queryParams;
        }

        /// <summary>
        /// Validate action properties that have <see cref="ActionPropertyAttribute"/> constraints
        /// </summary>
        /// <param name="errors">Properties that have validation errors</param>
        /// <param name="ignore">
        /// <para>Names of properties to ignore validation</para>
        /// <para>Names are case-sensitive</para>
        /// </param>
        /// <returns>Indicates if any validation errors</returns>
        public bool ValidateActionParams(out Dictionary<string, string> errors, ICollection<string>? ignore = null)
        {
            var res = new Dictionary<string, string>();
            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var property in properties)
            {
                var propAttr = property.GetCustomAttribute<ActionPropertyAttribute>();
                if (propAttr != null)
                {
                    if (ignore != null && ignore.Contains(propAttr.Name, StringComparer.Ordinal))
                        continue;

                    var value = property.GetValue(this);
                    if (propAttr.Required)
                    {
                        if (value == null)
                        {
                            // Required must never be null
                            res.Add(property.Name, $"Require value for \"{propAttr.Name}\"");
                        }
                        else if (value is string stringVal && string.IsNullOrEmpty(stringVal) && !propAttr.AllowEmpty)
                        {
                            // Required check if allow empty for values
                            res.Add(property.Name, $"Require value for \"{propAttr.Name}\"");
                        }
                        else if (value is IEnumerable enumerable && !enumerable.Cast<object>().Any() && !propAttr.AllowEmpty)
                        {
                            // Required check if allow empty for enumerable
                            res.Add(property.Name, $"Require value for \"{propAttr.Name}\"");
                        }
                    }

                    // TODO: Проверка типа значения
                }
            }

            errors = res;
            return !res.Any();
        }

        /// <summary>
        /// Format DateTime to yyyyMMddHHmmss string value
        /// <para>Nullable</para>
        /// </summary>
        public static string? FormatDateTime(DateTime? val) => val?.ToString(REQUEST_SEND_DATETIME_FORMAT);

        /// <summary>
        /// Format boolean to 'true' or 'false' string value
        /// <para>Nullable</para>
        /// </summary>
        public static string? FormatBool(bool? val) => val.HasValue ? val.Value ? "true" : "false" : null;

        /// <summary>
        /// Parse 'true' or 'false' string
        /// <para>Nullable</para>
        /// </summary>
        /// <param name="val">String value</param>
        /// <exception cref="FormatException"></exception>
        public static bool? ParseBool(string? val)
        {
            if (val == null)
                return null;
            
            if (val.Equals("true", StringComparison.OrdinalIgnoreCase))
                return true;

            if (val.Equals("false", StringComparison.OrdinalIgnoreCase))
                return false;
            
            throw new FormatException("Expected value for property are 'true', 'false' or null.");
        }

        /// <summary>
        /// Format KeyValue pair of parameters to JSON element
        /// </summary>
        /// <param name="jsonParams">Parameters collection</param>
        /// <returns>JSON element string</returns>
        public static string? FormatJsonParamsString(Dictionary<string, string>? jsonParams) => jsonParams != null
            ? $"{{{string.Join(",", jsonParams.Select(p => $"\"{p.Key}\":\"{p.Value}\""))}}}"
            : null;

        public static string? TryGetParamValue(Dictionary<string, string>? paramsDict, string paramKey)
        {
            if (paramsDict != null && paramsDict.TryGetValue(paramKey, out var value))
                return value;
            return null;
        }

        public static Dictionary<string, string>? TrySetParamValue(Dictionary<string, string>? paramsDict, string paramKey, string? paramVal)
        {
            if (string.IsNullOrEmpty(paramKey))
                throw new ArgumentException(nameof(paramKey));

            if (paramVal == null)
            {
                if (paramsDict != null)
                {
                    if (paramsDict.ContainsKey(paramKey) && paramsDict.Count > 1)
                        paramsDict.Remove(paramKey);
                    else
                        return null;
                }
            }
            else
            {
                if (paramsDict != null)
                    paramsDict.Add(paramKey, paramVal);
                else
                    return new Dictionary<string, string>() { { paramKey, paramVal } };
            }

            return paramsDict;
        }
    }

    public abstract class AlfabankAction<TResp> : AlfabankAction
        where TResp : class
    { }
}
