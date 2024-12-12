using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AlfabankMerchant.Models
{
    public class QrSbp
    {
        /// <summary>
        /// Идентификатор QR-кода
        /// </summary>
        [JsonProperty("qrId")]
        [JsonPropertyName("qrId")]
        public string? Id { get; set; }

        /// <summary>
        /// Состояние запроса QR-кода
        /// </summary>
        [JsonProperty("qrStatus")]
        [JsonPropertyName("qrStatus")]
        public QrStatusSbp? Status { get; set; }

        /// <summary>
        /// Содержимое зарегистрированного в СБП QR-кода
        /// <para>Присутствует, если значение qrStatus — STARTED</para>
        /// </summary>
        [JsonProperty("payload")]
        [JsonPropertyName("payload")]
        public string? Payload { get; set; }
    }
}
