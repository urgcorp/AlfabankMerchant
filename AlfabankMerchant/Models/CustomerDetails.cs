using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AlfabankMerchant.Models
{
    public class CustomerDetails
    {
        /// <summary>
        /// Электронная почта покупателя.
        /// <para>v: 01+</para>
        /// </summary>
        [JsonProperty("email")]
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        /// <para>Номер телефона покупателя.</para>
        /// <para>Если в телефон включён код страны, номер должен начинаться со знака плюс («+»).</para>
        /// <para>Если телефон передаётся без знака плюс («+»), то код страны указывать не следует.</para>
        /// <para>v: 01+</para>
        /// </summary>
        [JsonProperty("phone")]
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        /// <summary>
        /// <para>Фамилия, имя и отчество плательщика.</para>
        /// Параметр возвращается только в том случае, если был передан партнёром при регистрации.
        /// <para>v: 01+</para>
        /// </summary>
        [JsonProperty("fullName")]
        [JsonPropertyName("fullName")]
        public string? FullName { get; set; }

        /// <summary>
        /// Серия и номер паспорта плательщика в следующем формате: 2222888888.
        /// <para>Только для запросов по платежам с фискализацией.</para>
        /// <para>v: 01+</para>
        /// </summary>
        [JsonProperty("passport")]
        [JsonPropertyName("passport")]
        public string? Passport { get; set; }

        /// <summary>
        /// Идентификационный номер налогоплательщика.
        /// <para>Допускается передавать 10 или 12 символов.</para>
        /// <para>v: 01+</para>
        /// </summary>
        [JsonProperty("inn")]
        [JsonPropertyName("inn")]
        public long? INN { get; set; }

        /// <summary>
        /// Способ связи с покупателем.
        /// <para>v: 01+</para>
        /// </summary>
        [JsonProperty("contact")]
        [JsonPropertyName("contact")]
        public string? Contact { get; set; }

        /// <summary>
        /// Блок с атрибутами адреса для доставки.
        /// <para>v: 01+</para>
        /// </summary>
        [JsonProperty("deliveryInfo")]
        [JsonPropertyName("deliveryInfo")]
        public string? DeliveryInfo { get; set; }
    }
}
