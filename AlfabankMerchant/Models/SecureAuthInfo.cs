using Newtonsoft.Json;

namespace AlfabankMerchant.Models
{
    public class SecureAuthInfo
    {
        /// <summary>
        /// Электронный коммерческий индикатор
        /// </summary>
        [JsonProperty("eci")]
        public int? ECI { get; set; }

        [JsonProperty("paResStatus")]
        public string? paResStatus { get; set; }

        [JsonProperty("veResStatus")]
        public string? veResStatus { get; set; }

        /// <summary>
        /// Значение проверки аутентификации владельца карты
        /// </summary>
        [JsonIgnore]
        public string? CAVV => CAVV_VAL ?? TDSInfo?.CAVV;

        /// <summary>
        /// Электронный коммерческий идентификатор транзакции
        /// </summary>
        [JsonIgnore]
        public string? XID => XID_VAL ?? TDSInfo?.XID;

        [JsonProperty("threeDSInfo")]
        protected ThreeDSInfo? TDSInfo { get; set; }

        [JsonProperty("cavv")]
        protected string? CAVV_VAL { get; set; }

        [JsonProperty("xid")]
        protected string? XID_VAL { get; set; }
    }

    public class ThreeDSInfo
    {
        [JsonProperty("cavv")]
        public string? CAVV { get; set; }

        [JsonProperty("xid")]
        public string XID { get; set; }
    }
}
