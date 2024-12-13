using System.Diagnostics;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AlfabankMerchant.ComponentModel
{
    [DebuggerDisplay("{Name}")]
    public struct NameParamsProperty
    {
        /// <summary>
        /// Уникальное имя payment плагина
        /// <para>v: 28+</para>
        /// </summary>
        [JsonProperty("name")]
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; private set; }

        /// <summary>
        /// Параметры для конкретного платёжного метода. Параметр представляет собой словарь ключ: значение
        /// <para>v: 28+</para>
        /// </summary>
        [JsonProperty("params")]
        [JsonPropertyName("params")]
        [JsonInclude]
        public string ParamsJson { get; private set; }
    }
}
