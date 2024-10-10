using System.Diagnostics;
using Newtonsoft.Json;

namespace alfabank.ComponentModel
{
    [DebuggerDisplay("{Name}: {Value}")]
    public struct NameValueProperty
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        public NameValueProperty(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
