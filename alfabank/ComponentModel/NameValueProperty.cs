using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace alfabank.ComponentModel
{
    [DebuggerDisplay("{Name}: {Value}")]
    public class NameValueProperty
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
