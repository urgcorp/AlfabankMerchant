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
        /// <para>v: 03+</para>
        /// </summary>
        [JsonProperty("paymentState")]
        [JsonPropertyName("paymentState")]
        public string? PaymentState { get; set; }

        /// <summary>
        /// Сумма, подтверждённая к списанию
        /// <para>v: 03+</para>
        /// </summary>
        [JsonProperty("approvedAmount")]
        [JsonPropertyName("approvedAmount")]
        public long? ApprovedAmount { get; set; }

        /// <summary>
        /// Сумма списания с карты
        /// <para>v: 03+</para>
        /// </summary>
        [JsonProperty("depositedAmount")]
        [JsonPropertyName("depositedAmount")]
        public long? DepositedAmount { get; set; }

        /// <summary>
        /// Сумма возврата
        /// <para>v: 03+</para>
        /// </summary>
        [JsonProperty("refundedAmount")]
        [JsonPropertyName("refundedAmount")]
        public long? RefundedAmount { get; set; }

        /// <summary>
        /// Сумма комиссии
        /// <para>v: 11+</para>
        /// </summary>
        [JsonProperty("feeAmount")]
        [JsonPropertyName("feeAmount")]
        public long? FeeAmount { get; set; }

        /// <summary>
        /// Сумма заказа + комиссия, если она была использована в заказе
        /// <para>v: 18+</para>
        /// </summary>
        [JsonProperty("totalAmount")]
        [JsonPropertyName("totalAmount")]
        public long? TotalAmount { get; set; }
    }
}
