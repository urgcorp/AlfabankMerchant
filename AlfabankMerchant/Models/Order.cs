using System.Diagnostics;
using Newtonsoft.Json;
using AlfabankMerchant.Common;
using AlfabankMerchant.JsonConverter.Newtonsoft;
using System.Text.Json.Serialization;

namespace AlfabankMerchant.Models
{
    [DebuggerDisplay("{OrderNumber}: {Status} ({ActionCode})")]
    public class Order
    {
        /// <summary>
        /// Номер (идентификатор) заказа в системе магазина.
        /// </summary>
        [JsonProperty("orderNumber")]
        [JsonPropertyName("orderNumber")]
        public string OrderNumber { get; set; }

        /// <summary>
        /// Состояние заказа в платёжной системе.
        /// </summary>
        [JsonProperty("orderStatus")]
        [JsonPropertyName("orderStatus")]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Код ответа.
        /// </summary>
        [JsonProperty("actionCode")]
        [JsonPropertyName("actionCode")]
        public int? ActionCode { get; set; }

        /// <summary>
        /// Расшифровка кода ответа.
        /// </summary>
        [JsonProperty("actionCodeDescription")]
        [JsonPropertyName("actionCodeDescription")]
        public string? ActionCodeDescription { get; set; }

        /// <summary>
        /// Сумма платежа в копейках (или центах)
        /// </summary>
        [JsonProperty("amount")]
        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// Валюта платежа
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Currency Currency
        {
            get => Currency.Parse(CurrencyCode);
            set => CurrencyCode = value.CurrencyCode;
        }

        /// <summary>
        /// Код валюты платежа ISO 4217. Если не указан, считается равным валюте по умолчанию.
        /// </summary>
        [JsonProperty("currency")]
        [JsonPropertyName("currency")]
        protected int CurrencyCode { get; set; }

        /// <summary>
        /// Unix timestamp дата регистрации заказа в секундах
        /// </summary>
        [JsonProperty("date")]
        [JsonPropertyName("date")]
        protected string? DateUnixTS { get; set; }

        /// <summary>
        /// Дата регистрации заказа
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime? DateUtc => DateUnixTS != null ? DateTimeOffset.FromUnixTimeSeconds(long.Parse(DateUnixTS)).UtcDateTime : null;

        /// <summary>
        /// Описание заказа, переданное при его регистрации
        /// </summary>
        [JsonProperty("orderDescription")]
        [JsonPropertyName("orderDescription")]
        public string? Description { get; set; }

        /// <summary>
        /// IP адрес покупателя. Указан только после оплаты.
        /// </summary>
        [JsonProperty("ip")]
        [JsonPropertyName("ip")]
        public string? IpAddress { get; set; }

        /// <summary>
        /// Код ошибки
        /// </summary>
        [JsonProperty("errorCode")]
        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// Тэг с атрибутами, в которых передаются дополнительные параметры мерчанта
        /// </summary>
        [JsonProperty("merchantOrderParams")]
        [JsonPropertyName("merchantOrderParams")]
        [Newtonsoft.Json.JsonConverter(typeof(NameValuePropertyConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonConverter.NameValuePropertyConverter))]
        public Dictionary<string, string>? MerchantOrderParams { get; set; }

        /// <summary>
        /// Атрибуты заказа в платёжной системе (номер заказа)
        /// </summary>
        [JsonProperty("attributes")]
        [JsonPropertyName("attributes")]
        [Newtonsoft.Json.JsonConverter(typeof(NameValuePropertyConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonConverter.NameValuePropertyConverter))]
        public Dictionary<string, string>? Attributes { get; set; }

        /// <summary>
        /// Уникальный номер заказа в платёжной системе
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string? MdOrderNumber
        {
            get
            {
                if (Attributes != null && Attributes.ContainsKey("mdOrder"))
                    return Attributes["mdOrder"];
                return null;
            }
        }

        /// <summary>
        /// Тэг с атрибутами платежа
        /// </summary>
        [JsonProperty("cardAuthInfo")]
        [JsonPropertyName("cardAuthInfo")]
        public CardAuthInfo? CardAuthInfo { get; set; }

        /// <summary>
        /// Тэг с информацией о связке, с помощью которой осуществлена оплата
        /// </summary>
        [JsonProperty("bindingInfo")]
        [JsonPropertyName("bindingInfo")]
        public BindingInfo? BindingInfo { get; set; }

        /// <summary>
        /// Unix timestamp даты/времени авторизации в секундах
        /// </summary>
        [JsonProperty("authDateTime")]
        [JsonPropertyName("authDateTime")]
        protected string? AuthDateUnixTS { get; set; }

        /// <summary>
        /// Дата/время авторизации
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime? AuthDateUtc => AuthDateUnixTS != null
            ? DateTimeOffset.FromUnixTimeSeconds(long.Parse(AuthDateUnixTS)).UtcDateTime
            : null;

        /// <summary>
        /// Id терминала
        /// </summary>
        [JsonProperty("terminalId")]
        [JsonPropertyName("terminalId")]
        public string? TerminalId { get; set; }

        /// <summary>
        /// Reference number
        /// </summary>
        [JsonProperty("authRefNum")]
        [JsonPropertyName("authRefNum")]
        public string? AuthRefNumber { get; set; }

        /// <summary>
        /// Тэг с информацией о суммах подтверждения, списания, возврата
        /// </summary>
        [JsonProperty("paymentAmountInfo")]
        [JsonPropertyName("paymentAmountInfo")]
        public PaymentAmountInfo? PaymentAmountInfo { get; set; }

        /// <summary>
        /// Тэг с информацией о Банке-эмитенте
        /// </summary>
        [JsonProperty("bankInfo")]
        [JsonPropertyName("bankInfo")]
        public BankInfo? BankInfo { get; set; }
    }
}
