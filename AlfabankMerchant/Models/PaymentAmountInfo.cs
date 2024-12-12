using System.Diagnostics;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AlfabankMerchant.Models
{
    [DebuggerDisplay("{PaymentState}")]
    public class PaymentAmountInfo
    {
        /// <summary>
        /// Состояние платежа
        /// </summary>
        [JsonProperty("paymentState")]
        [JsonPropertyName("paymentState")]
        public string? PaymentState { get; set; }

        /// <summary>
        /// Сумма, подтверждённая к списанию
        /// </summary>
        [JsonProperty("approvedAmount")]
        [JsonPropertyName("approvedAmount")]
        public int? ApprovedAmount { get; set; }

        /// <summary>
        /// Сумма списания с карты
        /// </summary>
        [JsonProperty("depositedAmount")]
        [JsonPropertyName("depositedAmount")]
        public int? DepositedAmount { get; set; }

        /// <summary>
        /// Сумма возврата
        /// </summary>
        [JsonProperty("refundedAmount")]
        [JsonPropertyName("refundedAmount")]
        public int? RefundedAmount { get; set; }

        /// <summary>
        /// Сумма комиссии
        /// </summary>
        [JsonProperty("feeAmount")]
        [JsonPropertyName("feeAmount")]
        public int? FeeAmount { get; set; }
    }
}
