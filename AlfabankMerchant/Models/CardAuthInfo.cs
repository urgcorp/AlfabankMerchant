using System.Diagnostics;
using System.Text.Json.Serialization;
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
        public string PAN { get; set; }

        /// <summary>
        /// Срок истечения действия карты в формате YYYYMM
        /// </summary>
        [JsonProperty("expiration")]
        [JsonPropertyName("expiration")]
        public string Expiration { get; set; }

        /// <summary>
        /// Имя держателя карты
        /// </summary>
        [JsonProperty("cardholderName")]
        [JsonPropertyName("cardholderName")]
        public string CardholderName { get; set; }

        /// <summary>
        /// <para>Код авторизации платежа</para>
        /// <para>Поле фиксированной длины (6 символов), может содержать цифры и латинские буквы</para>
        /// </summary>
        [JsonProperty("approvalCode")]
        [JsonPropertyName("approvalCode")]
        public string ApprovalCode { get; set; }

        /// <summary>
        /// Возможны следующие значения: true (истина); false (ложь)
        /// </summary>
        [JsonProperty("chargeback")]
        [JsonPropertyName("chargeback")]
        protected string? ChargebackRaw { get; set; }

        /// <summary>
        /// Были ли средства принудительно возвращены покупателю банком
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public bool? Chargeback
        {
            get
            {
                if (ChargebackRaw == null)
                    return null;
                if (ChargebackRaw.Equals("true", StringComparison.OrdinalIgnoreCase))
                    return true;
                if (ChargebackRaw.Equals("false", StringComparison.OrdinalIgnoreCase))
                    return false;
                throw new ArgumentOutOfRangeException("chargeback", $"Value \"{ChargebackRaw}\" is not recognized");
            }
        }

        /// <summary>
        /// <para>Наименование платёжной системы</para>
        /// Доступны следующие варианты: VISA, MASTERCARD, AMEX, JCB, CUP, MIR
        /// </summary>
        [JsonProperty("paymentSystem")]
        [JsonPropertyName("paymentSystem")]
        public string? PaymentSystem { get; set; }

        /// <summary>
        /// Дополнительные сведения о корпоративных картах. Эти сведения заполняются службой технической поддержки в консоли управления
        /// <para>Если такие сведения отсутствуют, возвращается пустое значение</para>
        /// </summary>
        [JsonProperty("product")]
        [JsonPropertyName("product")]
        public string? Product { get; set; }

        /// <summary>
        /// Способ совершения платежа (платёж в с вводом карточных данных, оплата по связке и т. п.)
        /// </summary>
        [JsonProperty("paymentWay")]
        [JsonPropertyName("paymentWay")]
        public string? PaymentWay { get; set; }

        /// <summary>
        /// 
        /// <para>Указан только после оплаты заказа и в случае соответствующего разрешения</para>
        /// </summary>
        [JsonProperty("secureAuthInfo")]
        [JsonPropertyName("secureAuthInfo")]
        public SecureAuthInfo? SecureAuthInfo { get; set; }
    }
}
