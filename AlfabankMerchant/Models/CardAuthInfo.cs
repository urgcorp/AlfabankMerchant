using System.Diagnostics;
using System.Text.Json.Serialization;
using AlfabankMerchant.Common;
using Newtonsoft.Json;

namespace AlfabankMerchant.Models
{
    [DebuggerDisplay("{PAN}")]
    public class CardAuthInfo
    {
        /// <summary>
        /// Маскированный номер карты, которая использовалась для оплаты
        /// </summary>
        [JsonProperty("maskedPan")]
        [JsonPropertyName("maskedPan")]
        public string? PAN { get; set; }

        /// <summary>
        /// Срок истечения действия карты в формате YYYYMM
        /// </summary>
        [JsonProperty("expiration")]
        [JsonPropertyName("expiration")]
        public int? Expiration { get; set; }

        /// <summary>
        /// Имя держателя карты
        /// </summary>
        [JsonProperty("cardholderName")]
        [JsonPropertyName("cardholderName")]
        public string? CardholderName { get; set; }

        /// <summary>
        /// <para>Код авторизации платежа</para>
        /// <para>Поле фиксированной длины (6 символов), может содержать цифры и латинские буквы</para>
        /// </summary>
        [JsonProperty("approvalCode")]
        [JsonPropertyName("approvalCode")]
        public string? ApprovalCode { get; set; }

        /// <summary>
        /// <para>Наименование платёжной системы</para>
        /// <para>v: 08+</para>
        /// </summary>
        [JsonProperty("paymentSystem")]
        [JsonPropertyName("paymentSystem")]
        public PaymentSystem? PaymentSystem { get; set; }

        /// <summary>
        /// Дополнительные сведения о корпоративных картах. Эти сведения заполняются службой технической поддержки в консоли управления
        /// <para>Если такие сведения отсутствуют, возвращается пустое значение</para>
        /// <para>v: 08+</para>
        /// </summary>
        [JsonProperty("product")]
        [JsonPropertyName("product")]
        public string? Product { get; set; }

        /// <summary>
        /// Дополнительные сведения о категории корпоративных карт. Эти сведения заполняются службой технической поддержки в консоли управления.
        /// <para>Если такие сведения отсутствуют, возвращается пустое значение.</para>
        /// <para>Возможные значения: DEBIT, CREDIT, PREPAID, NON_MASTERCARD, CHARGE, DIFFERED_DEBIT</para>
        /// <para>v: 17+</para>
        /// </summary>
        [JsonProperty("productCategory")]
        [JsonPropertyName("productCategory")]
        public string? ProductCategory { get; set; }

        /// <summary>
        /// Признак того, является ли карта корпоративной.
        /// <para>v: 35+</para>
        /// </summary>
        [JsonProperty("corporateCard")]
        [JsonPropertyName("corporateCard")]
        [Newtonsoft.Json.JsonConverter(typeof(JsonConverter.Newtonsoft.BooleanStringConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonConverter.BooleanStringConverter))]
        public bool? CorporateCard { get; set; }

        /// <summary>
        /// Способ совершения платежа (платёж в с вводом карточных данных, оплата по связке и т. п.)
        /// </summary>
        [JsonProperty("paymentWay")]
        [JsonPropertyName("paymentWay")]
        public PaymentWay? PaymentWay { get; set; }

        /// <summary>
        /// Были ли средства принудительно возвращены покупателю банком
        /// </summary>
        [JsonProperty("chargeback")]
        [JsonPropertyName("chargeback")]
        [Newtonsoft.Json.JsonConverter(typeof(JsonConverter.Newtonsoft.BooleanStringConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonConverter.BooleanStringConverter))]
        protected bool? Chargeback { get; set; }

        /// <summary>
        /// 
        /// <para>Указан только после оплаты заказа и в случае соответствующего разрешения</para>
        /// </summary>
        [JsonProperty("secureAuthInfo")]
        [JsonPropertyName("secureAuthInfo")]
        public SecureAuthInfo? SecureAuthInfo { get; set; }
    }
}
