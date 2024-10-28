using System.Collections;
using System.Reflection;

namespace AlfabankMerchant.ComponentModel
{
    public abstract class AlfabankAction
    {
        public const string REQUEST_SEND_DATETIME_FORMAT = "yyyyMMddHHmmss";

        public string? FindDefaultActionUrl(string clientType)
        {
            if (!string.IsNullOrEmpty(ActionUrl))
                return ActionUrl;

            var attributes = Attribute.GetCustomAttributes(GetType(), typeof(ActionUrlAttribute)).Cast<ActionUrlAttribute>()
                .Cast<ActionUrlAttribute>();
            if (attributes.Any())
                return (attributes.FirstOrDefault(x => x.ClientType.Equals(clientType, StringComparison.OrdinalIgnoreCase)))?.Url;

            return null;
        }

        public virtual string? ActionUrl { get; set; }

        public ActionAuthorizationAttribute? GetAuthorizationConfig() => (ActionAuthorizationAttribute?)Attribute.GetCustomAttribute(GetType(), typeof(ActionAuthorizationAttribute));

        /// <summary></summary>
        /// <param name="authorization">Override authorization properties</param>
        /// <returns>Set or parameters to send with request</returns>
        public Dictionary<string, string> GetActionParams(AuthParams? authorization = null)
        {
            var authAttribute = GetAuthorizationConfig();
            var queryParams = new Dictionary<string, string>();

            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var property in properties)
            {
                var propAttr = property.GetCustomAttribute<ActionPropertyAttribute>();
                if (propAttr != null)
                {
                    if (authAttribute != null)
                    {
                        if (propAttr.Name == "token")
                        {
                            if (!authAttribute.Allowed.Contains(AuthMethod.TOKEN))
                                continue;
                            if (!string.IsNullOrEmpty(authorization?.Token))
                            {
                                if (authAttribute.Priority == AuthMethod.LOGIN && !string.IsNullOrEmpty(authorization.Login))
                                    continue;

                                queryParams[propAttr.Name] = authorization.Token;
                                continue;
                            }

                        }
                        else if (propAttr.Name == "userName" || propAttr.Name == "password")
                        {
                            if (!authAttribute.Allowed.Contains(AuthMethod.LOGIN))
                                continue;
                            if (!string.IsNullOrEmpty(authorization?.Login))
                            {
                                if (authAttribute.Priority == AuthMethod.TOKEN && !string.IsNullOrEmpty(authorization.Token))
                                    continue;

                                if (propAttr.Name == "userName")
                                    queryParams[propAttr.Name] = authorization.Login;
                                else
                                    queryParams[propAttr.Name] = authorization.Password ?? "";
                            }
                        }
                    }

                    var value = property.GetValue(this);
                    if (value != null)
                    {
                        if (value is string)
                            queryParams[propAttr.Name] = (string)value;
                        else
                            queryParams[propAttr.Name] = value.ToString()!;
                    }
                }
            }

            return queryParams;
        }

        public bool ValidateActionParams(out Dictionary<string, string> errors)
        {
            var res = new Dictionary<string, string>();
            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var property in properties)
            {
                var propAttr = property.GetCustomAttribute<ActionPropertyAttribute>();
                if (propAttr != null)
                {
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

        [ActionProperty("userName", Type = "AN..30")]
        protected string? Login { get; set; }

        [ActionProperty("password", Type = "AN..30")]
        protected string? Password { get; set; }

        /// <summary>
        /// Открытый ключ, который можно использовать для регистрации заказа.
        /// <para>Если для аутентификации при регистрации заказа используются логин и пароль, параметр token передавать не нужно</para>
        /// </summary>
        [ActionProperty("token", Type = "AN..30")]
        protected string? Token { get; set; }

        /// <summary>
        /// yyyyMMddHHmmss DateTime format
        /// </summary>
        protected static string? GetDTParamValue(DateTime? val) => val.HasValue ? val.Value.ToString(REQUEST_SEND_DATETIME_FORMAT) : null;

        protected static string? GetBoolParamVal(bool? val) => val.HasValue ? val.Value ? "true" : "false" : null;

        protected static string? GetJsonParamsString(Dictionary<string, string>? jsonParams) => jsonParams != null
            ? $"{{{string.Join(",", jsonParams.Select(p => $"\"{p.Key}\":\"{p.Value}\""))}}}"
            : null;

        protected static string? TryGetParamValue(Dictionary<string, string>? paramsDict, string paramKey)
        {
            if (paramsDict != null && paramsDict.ContainsKey(paramKey))
                return paramsDict[paramKey];
            return null;
        }

        protected static Dictionary<string, string>? TrySetParamValue(Dictionary<string, string>? paramsDict, string paramKey, string? paramVal)
        {
            if (string.IsNullOrEmpty(paramKey))
                throw new ArgumentException();

            if (paramVal == null)
            {
                if (paramsDict != null)
                {
                    if (paramsDict.ContainsKey("subscriptionPurpose") && paramsDict.Count > 1)
                        paramsDict.Remove("subscriptionPurpose");
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
