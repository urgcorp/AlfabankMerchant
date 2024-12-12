using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AlfabankMerchant.Models.Response
{
    /// <summary>
    /// Состояние запроса в СБП
    /// </summary>
    public class SbpQrStatusResponse
    {
        /// <summary>
        /// Тип QR-кода
        /// <para>STATIC - статический</para>
        /// <para>DYNAMIC - динамический</para>
        /// <para>В настоящее время всегда возвращается значение DYNAMIC</para>
        /// </summary>
        [JsonProperty("qrType")]
        [JsonPropertyName("qrType")]
        public string? QrType { get; set; }

        /// <summary>
        /// Состояние запроса QR-кода
        /// </summary>
        [JsonProperty("qrStatus")]
        [JsonPropertyName("qrStatus")]
        public QrStatusSbp? Status { get; set; }

        /// <summary>
        /// Статус заказа
        /// <para>CREATED - создан</para>
        /// <para>DECLINED - отклонён</para>
        /// <para>DEPOSITED - оплачен</para>
        /// </summary>
        [JsonProperty("transactionState")]
        [JsonPropertyName("transactionState")]
        public string? TransactionState { get; set; }
    }
}
