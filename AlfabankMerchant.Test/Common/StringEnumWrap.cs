using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AlfabankMerchant.Test.Common;

public class StringEnumWrap
{
    [JsonProperty("single")]
    [JsonPropertyName("single")]
    public TestStringEnum Single { get; set; } = TestStringEnum.One;
        
    [JsonProperty("multiple")]
    [JsonPropertyName("multiple")]
    public TestStringEnum[] Multiple { get; set; } = new[] { TestStringEnum.Three, TestStringEnum.Two, TestStringEnum.One };
}