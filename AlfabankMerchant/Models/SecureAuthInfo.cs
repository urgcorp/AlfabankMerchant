using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AlfabankMerchant.Models
{
    public class SecureAuthInfo
    {
        /// <summary>
        /// Электронный коммерческий индикатор
        /// </summary>
        [JsonProperty("eci")]
        [JsonPropertyName("eci")]
        public int? ECI { get; set; }

        [JsonProperty("paResStatus")]
        [JsonPropertyName("paResStatus")]
        public string? paResStatus { get; set; }

        [JsonProperty("veResStatus")]
        [JsonPropertyName("veResStatus")]
        public string? veResStatus { get; set; }

        /// <summary>
        /// Значение проверки аутентификации владельца карты
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string? CAVV => CAVV_VAL ?? TDSInfo?.CAVV;

        /// <summary>
        /// Электронный коммерческий идентификатор транзакции
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string? XID => XID_VAL ?? TDSInfo?.XID;

        [JsonProperty("threeDSInfo")]
        [JsonPropertyName("threeDSInfo")]
        protected ThreeDSInfo? TDSInfo { get; set; }

        [JsonProperty("cavv")]
        [JsonPropertyName("cavv")]
        protected string? CAVV_VAL { get; set; }

        [JsonProperty("xid")]
        [JsonPropertyName("xid")]
        protected string? XID_VAL { get; set; }
    }

    public class ThreeDSInfo
    {
        [JsonProperty("cavv")]
        [JsonPropertyName("cavv")]
        public string? CAVV { get; set; }

        [JsonProperty("xid")]
        [JsonPropertyName("xid")]
        public string XID { get; set; }
    }
}
