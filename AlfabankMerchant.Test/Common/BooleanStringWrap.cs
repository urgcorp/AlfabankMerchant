using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace AlfabankMerchant.Test.Common;

public class BooleanStringWrap
{
    [JsonProperty("strValue")]
    [JsonPropertyName("strValue")]
    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter.Newtonsoft.BooleanStringConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonConverter.BooleanStringConverter))]
    public bool? BValue { get; set; }
    
    public static string FormatJsonObject(string? strBoolValue)
    {
        return strBoolValue != null
            ? $"{{ \"strValue\": \"{strBoolValue}\" }}"
            : "{ \"strValue\": null }";
    }

    public static readonly Regex PropertyRegex = new Regex(@"{.*?""strValue""\s*:\s*(null|""[^""]*"").*?}");

    public static bool FindPropValueFromJson(string jsonObject, out string outerProperty)
    {
        var res = PropertyRegex.Match(jsonObject);
        outerProperty = res.Groups[1].Value;
        return res.Success;
    }
}