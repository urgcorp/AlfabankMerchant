using System.Diagnostics;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AlfabankMerchant.ComponentModel
{
    [DebuggerDisplay("{Name}: {Value}")]
    public struct NameValueProperty
    {
        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string Name { get; private set; }

        [JsonProperty("value")]
        [JsonPropertyName("value")]
        public string Value { get; set; }

        public NameValueProperty(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
