using System.Text.Json.Serialization;
using Newtonsoft.Json;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.Test.Common;

[RestUrl("https://example.com")]
public class AlfabankActionBlank : AlfabankAction
{
    [ActionProperty("asn512", Type = "ANS..512")]
    [JsonPropertyName("asn512")]
    [JsonProperty("asn512")]
    public string? StringAny { get; set; }

    public AlfabankActionBlank()
    { }

    public AlfabankActionBlank(string? login, string? password, string? token)
        : base(login, password, token)
    { }
}