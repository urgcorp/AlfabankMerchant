using System.Diagnostics;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AlfabankMerchant.Models
{
    [DebuggerDisplay("[{CountryCode}] {PostAddress}")]
    public class DeliveryInfo
    {
        /// <summary>
        /// Тип доставки.
        /// <para>v: 01+</para>
        /// </summary>
        [JsonProperty("deliveryType")]
        [JsonPropertyName("deliveryType")]
        public string? DelivaryType { get; set; }

        /// <summary>
        /// Страна доставки.
        /// <para>v: 01+</para>
        /// </summary>
        [JsonProperty("country")]
        [JsonPropertyName("country")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Город доставки.
        /// <para>v: 01+</para>
        /// </summary>
        [JsonProperty("city")]
        [JsonPropertyName("city")]
        public string City { get; set; }

        /// <summary>
        /// Адрес доставки товара.
        /// <para>v: 01+</para>
        /// </summary>
        [JsonProperty("postAddress")]
        [JsonPropertyName("postAddress")]
        public string PostAddress { get; set; }
    }
}
