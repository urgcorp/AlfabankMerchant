using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AlfabankMerchant.Test.Common;

public class NameValuePropertyWrap
{
    [JsonProperty("kvAttr")]
    [JsonPropertyName("kvAttr")]
    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter.Newtonsoft.NameValuePropertyConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonConverter.NameValuePropertyConverter))]
    public Dictionary<string, string>? Attributes { get; set; }
}