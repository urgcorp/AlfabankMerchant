using System.Diagnostics;
using Newtonsoft.Json;

namespace alfabank.Models
{
    [DebuggerDisplay("{CountryCode}| {Name}")]
    public class BankInfo
    {
        /// <summary>
        /// Наименование Банка-эмитента
        /// </summary>
        [JsonProperty("bankName")]
        public string Name { get; set; }

        /// <summary>
        /// Код страны Банка-эмитента
        /// </summary>
        [JsonProperty("bankCountryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Наименование страны банка-эмитента на языке, переданном в параметре language в запросе, или на языке пользователя, вызвавшего метод, если язык в запросе не указан
        /// </summary>
        [JsonProperty("bankCountryName")]
        public string? CountryName { get; set; }
    }
}
