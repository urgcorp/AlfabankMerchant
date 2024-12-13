using System.Diagnostics;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AlfabankMerchant.Models
{
    [DebuggerDisplay("{CountryCode}| {Name}")]
    public class BankInfo
    {
        /// <summary>
        /// Наименование Банка-эмитента
        /// </summary>
        [JsonProperty("bankName")]
        [JsonPropertyName("bankName")]
        public string? Name { get; set; }

        /// <summary>
        /// Код страны Банка-эмитента
        /// </summary>
        [JsonProperty("bankCountryCode")]
        [JsonPropertyName("bankCountryCode")]
        public string? CountryCode { get; set; }

        /// <summary>
        /// Наименование страны банка-эмитента на языке, переданном в параметре language в запросе, или на языке пользователя, вызвавшего метод, если язык в запросе не указан
        /// </summary>
        [JsonProperty("bankCountryName")]
        [JsonPropertyName("bankCountryName")]
        public string? CountryName { get; set; }
    }
}
