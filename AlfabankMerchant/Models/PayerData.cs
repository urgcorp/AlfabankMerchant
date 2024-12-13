using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AlfabankMerchant.Models
{
    public class PayerData
    {
        /// <summary>
        /// Электронная почта покупателя.
        /// <para>v: 13+</para>
        /// </summary>
        [JsonProperty("email")]
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        /// <para>Номер телефона покупателя.</para>
        /// Всегда нужно указывать код страны, при этом можно указывать или не указывать знак
        /// <para>v: 13+</para>
        /// </summary>
        [JsonProperty("phone")]
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        /// <summary>
        /// Адрес доставки товара.
        /// <para>v: 13+</para>
        /// </summary>
        [JsonProperty("postAddress")]
        [JsonPropertyName("postAddress")]
        public string? PostAddress { get; set; }
    }
}
