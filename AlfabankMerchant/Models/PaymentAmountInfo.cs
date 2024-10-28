using System.Diagnostics;
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
        public string? PaymentState { get; set; }

        /// <summary>
        /// Сумма, подтверждённая к списанию
        /// </summary>
        [JsonProperty("approvedAmount")]
        public int? ApprovedAmount { get; set; }

        /// <summary>
        /// Сумма списания с карты
        /// </summary>
        [JsonProperty("depositedAmount")]
        public int? DepositedAmount { get; set; }

        /// <summary>
        /// Сумма возврата
        /// </summary>
        [JsonProperty("refundedAmount")]
        public int? RefundedAmount { get; set; }

        /// <summary>
        /// Сумма комиссии
        /// </summary>
        [JsonProperty("feeAmount")]
        public int? FeeAmount { get; set; }
    }
}
