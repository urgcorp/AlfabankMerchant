using Newtonsoft.Json;

namespace AlfabankMerchant.Models
{
    public class QrSbp
    {
        /// <summary>
        /// Идентификатор QR-кода
        /// </summary>
        [JsonProperty("qrId")]
        public string? Id { get; set; }

        /// <summary>
        /// Состояние запроса QR-кода
        /// </summary>
        [JsonProperty("qrStatus")]
        public QrStatusSbp? Status { get; set; }

        /// <summary>
        /// Содержимое зарегистрированного в СБП QR-кода
        /// <para>Присутствует, если значение qrStatus — STARTED</para>
        /// </summary>
        [JsonProperty("payload")]
        public string? Payload { get; set; }
    }
}
