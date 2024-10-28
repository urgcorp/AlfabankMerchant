using System.Diagnostics;
using Newtonsoft.Json;
using AlfabankMerchant.JsonConverter;

namespace AlfabankMerchant.Models
{
    [DebuggerDisplay("{OrderNumber}: {Status} ({ActionCode})")]
    public class Order
    {
        /// <summary>
        /// Номер (идентификатор) заказа в системе магазина.
        /// </summary>
        [JsonProperty("orderNumber")]
        public string OrderNumber { get; set; }

        /// <summary>
        /// Состояние заказа в платёжной системе.
        /// </summary>
        [JsonProperty("orderStatus")]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Код ответа.
        /// </summary>
        [JsonProperty("actionCode")]
        public int? ActionCode { get; set; }

        /// <summary>
        /// Расшифровка кода ответа.
        /// </summary>
        [JsonProperty("actionCodeDescription")]
        public string? ActionCodeDescription { get; set; }

        /// <summary>
        /// Сумма платежа в копейках (или центах)
        /// </summary>
        [JsonProperty("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// Валюта платежа
        /// </summary>
        [JsonIgnore]
        public Currency Currency
        {
            get => Currency.Parse(CurrencyCode);
            set => CurrencyCode = value.CurrencyCode;
        }

        /// <summary>
        /// Код валюты платежа ISO 4217. Если не указан, считается равным валюте по умолчанию.
        /// </summary>
        [JsonProperty("currency")]
        protected int CurrencyCode { get; set; }

        /// <summary>
        /// Unix timestamp дата регистрации заказа в секундах
        /// </summary>
        [JsonProperty("date")]
        protected string? DateUnixTS { get; set; }

        /// <summary>
        /// Дата регистрации заказа
        /// </summary>
        [JsonIgnore]
        public DateTime? DateUtc => DateUnixTS != null ? DateTimeOffset.FromUnixTimeSeconds(long.Parse(DateUnixTS)).UtcDateTime : null;

        /// <summary>
        /// Описание заказа, переданное при его регистрации
        /// </summary>
        [JsonProperty("orderDescription")]
        public string? Description { get; set; }

        /// <summary>
        /// IP адрес покупателя. Указан только после оплаты.
        /// </summary>
        [JsonProperty("ip")]
        public string? IpAddress { get; set; }

        /// <summary>
        /// Код ошибки
        /// </summary>
        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// Тэг с атрибутами, в которых передаются дополнительные параметры мерчанта
        /// </summary>
        [JsonProperty("merchantOrderParams")]
        [JsonConverter(typeof(NameValuePropertyConverter))]
        public Dictionary<string, string>? MerchantOrderParams { get; set; }

        /// <summary>
        /// Атрибуты заказа в платёжной системе (номер заказа)
        /// </summary>
        [JsonProperty("attributes")]
        [JsonConverter(typeof(NameValuePropertyConverter))]
        public Dictionary<string, string>? Attributes { get; set; }

        /// <summary>
        /// Уникальный номер заказа в платёжной системе
        /// </summary>
        [JsonIgnore]
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
        public CardAuthInfo? CardAuthInfo { get; set; }

        /// <summary>
        /// Тэг с информацией о связке, с помощью которой осуществлена оплата
        /// </summary>
        [JsonProperty("bindingInfo")]
        public BindingInfo? BindingInfo { get; set; }

        /// <summary>
        /// Unix timestamp даты/времени авторизации в секундах
        /// </summary>
        [JsonProperty("authDateTime")]
        protected string? AuthDateUnixTS { get; set; }

        /// <summary>
        /// Дата/время авторизации
        /// </summary>
        [JsonIgnore]
        public DateTime? AuthDateUtc => AuthDateUnixTS != null
            ? DateTimeOffset.FromUnixTimeSeconds(long.Parse(AuthDateUnixTS)).UtcDateTime
            : null;

        /// <summary>
        /// Id терминала
        /// </summary>
        [JsonProperty("terminalId")]
        public string? TerminalId { get; set; }

        /// <summary>
        /// Reference number
        /// </summary>
        [JsonProperty("authRefNum")]
        public string? AuthRefNumber { get; set; }

        /// <summary>
        /// Тэг с информацией о суммах подтверждения, списания, возврата
        /// </summary>
        [JsonProperty("paymentAmountInfo")]
        public PaymentAmountInfo? PaymentAmountInfo { get; set; }

        /// <summary>
        /// Тэг с информацией о Банке-эмитенте
        /// </summary>
        [JsonProperty("bankInfo")]
        public BankInfo? BankInfo { get; set; }
    }
}
