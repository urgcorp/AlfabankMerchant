using System.Diagnostics;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AlfabankMerchant.Models
{
    [DebuggerDisplay("{ECI} [{AuthType}]")]
    public class SecureAuthInfo
    {
        /// <summary>
        /// Электронный коммерческий индикатор
        /// <para>v: 01+</para>
        /// </summary>
        [JsonProperty("eci")]
        [JsonPropertyName("eci")]
        public int? ECI { get; set; }

        /// <summary>
        /// Тип 3DS аутентификации
        /// <para>v: 01+</para>
        /// </summary>
        [JsonProperty("authTypeIndicator")]
        [JsonPropertyName("authTypeIndicator")]
        public int? AuthType { get; set; }

        /// <summary>
        /// Версия протокола 3DS.
        /// <para>v: 30+</para>
        /// </summary>
        [JsonProperty("threeDsProtocolVersion")]
        [JsonPropertyName("threeDsProtocolVersion")]
        public string? TDSProtoVersion { get; set; }

        /// <summary>
        /// Статус транзакции из запроса для передачи результатов аутентификации пользователя от ACS (RReq).
        /// <para>Передаётся при использовании 3DS2.</para>
        /// <para>v: 30+</para>
        /// </summary>
        [JsonProperty("rreqTransStatus")]
        [JsonPropertyName("rreqTransStatus")]
        public string? RReqTransStatus { get; set; }

        /// <summary>
        /// Статус транзакции из ответа от ACS на запрос аутентификации (ARes). Передаётся при использовании 3DS2.
        /// <para>v: 30+</para>
        /// </summary>
        [JsonProperty("aresTransStatus")]
        [JsonPropertyName("aresTransStatus")]
        public string? AResTransStatus { get; set; }

        /// <summary>
        /// <para>v: 01+</para>
        /// </summary>
        [JsonProperty("threeDSInfo")]
        [JsonPropertyName("threeDSInfo")]
        [JsonInclude]
        protected ThreeDSInfo? TDSInfo { get; set; }

        [JsonProperty("paResStatus")]
        [JsonPropertyName("paResStatus")]
        public string? paResStatus { get; set; }

        [JsonProperty("veResStatus")]
        [JsonPropertyName("veResStatus")]
        public string? veResStatus { get; set; }

        [JsonProperty("cavv")]
        [JsonPropertyName("cavv")]
        [JsonInclude]
        protected string? CAVV_VAL { get; set; }

        [JsonProperty("xid")]
        [JsonPropertyName("xid")]
        [JsonInclude]
        protected string? XID_VAL { get; set; }

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
    }

    public class ThreeDSInfo
    {
        /// <summary>
        /// Значение проверки аутентификации владельца карты.
        /// <para>Указан только после оплаты заказа и в случае соответствующего разрешения.</para>
        /// <para>v: 01+</para>
        /// </summary>
        [JsonProperty("cavv")]
        [JsonPropertyName("cavv")]
        public string? CAVV { get; set; }

        /// <summary>
        /// Электронный коммерческий идентификатор транзакции.
        /// <para>v: 01+</para>
        /// </summary>
        [JsonProperty("xid")]
        [JsonPropertyName("xid")]
        public string? XID { get; set; }
    }
}
