using System.Text.Json.Serialization;
using Newtonsoft.Json;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant;

public abstract class AlfabankAction
{
    public const string REQUEST_SEND_DATETIME_FORMAT = "yyyyMMddHHmmss";

    /// <summary>
    /// Override URL path for action
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual string? ActionUrl { get; set; }

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

    #region Property helpers

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
    protected static string? FormatJsonParamsString(Dictionary<string, string>? jsonParams) => jsonParams != null
        ? $"{{{string.Join(",", jsonParams.Select(p => $"\"{p.Key}\":\"{p.Value}\""))}}}"
        : null;

    protected static string? TryGetParamValue(Dictionary<string, string>? paramsDict, string paramKey)
    {
        if (paramsDict != null && paramsDict.TryGetValue(paramKey, out var value))
            return value;
        return null;
    }

    protected static Dictionary<string, string>? TrySetParamValue(Dictionary<string, string>? paramsDict, string paramKey, string? paramVal)
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
        
    #endregion
}

public abstract class AlfabankAction<TResp> : AlfabankAction
    where TResp : class
{ }