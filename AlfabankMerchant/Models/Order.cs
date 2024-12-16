using System.Diagnostics;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using AlfabankMerchant.Common;
using AlfabankMerchant.JsonConverter.Newtonsoft;

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
        public string OrderNumber { get; set; } = null!;

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
        /// Код ответа процессинга.
        /// </summary>
        [JsonProperty("originalActionCode")]
        [JsonPropertyName("originalActionCode")]
        public string? OriginalActionCode { get; set; }

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
        [JsonInclude]
        protected int CurrencyCode { get; set; }

        /// <summary>
        /// Unix timestamp дата регистрации заказа в секундах
        /// </summary>
        [JsonProperty("date")]
        [JsonPropertyName("date")]
        [JsonInclude]
        protected long? DateUnixTS { get; set; }

        /// <summary>
        /// Дата регистрации заказа
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime? DateUtc => DateUnixTS != null ? DateTimeOffset.FromUnixTimeSeconds(DateUnixTS.Value).UtcDateTime : null;

        /// <summary>
        /// Дата оплаты заказа в формате UNIX-времени
        /// </summary>
        [JsonProperty("depositedDate")]
        [JsonPropertyName("depositedDate")]
        [JsonInclude]
        protected long? DepositedDateUnixTs { get; set; }

        /// <summary>
        /// Дата оплаты заказа
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime? DepositedDateUtc => DepositedDateUnixTs != null ? DateTimeOffset.FromUnixTimeSeconds(DepositedDateUnixTs.Value).UtcDateTime : null;

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
                if (Attributes != null && Attributes.TryGetValue("mdOrder", out var number))
                    return number;
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
        /// Дата и время возврата средств.
        /// </summary>
        [JsonProperty("refundedDate")]
        [JsonPropertyName("refundedDate")]
        public string? RefundDate { get; set; }

        /// <summary>
        /// Дата и время отмены платежа.
        /// </summary>
        [JsonProperty("reversedDate")]
        [JsonPropertyName("reversedDate")]
        public string? ReversedDate { get; set; }

        /// <summary>
        /// Способ совершения платежа
        /// </summary>
        [JsonProperty("paymentWay")]
        [JsonPropertyName("paymentWay")]
        public PaymentWay? PaymentWay { get; set; }

        //[JsonInclude]
        //protected string? PaymentWayValue { get; set; }

        /// <summary>
        /// Уникальный идентификатор заказа на предоплату в Платёжном Шлюзе.
        /// <para>Используется для привязки заказа с предоплатой с чеком на пост-оплату.</para>
        /// </summary>
        [JsonProperty("prepaymentMdOrder")]
        [JsonPropertyName("prepaymentMdOrder")]
        public string? PrepaymentMdOrder { get; set; }

        /// <summary>
        /// mdOrder последующих заказов на частичную оплату
        /// </summary>
        [JsonProperty("partpaymentMdOrders")]
        [JsonPropertyName("partpaymentMdOrders")]
        public string? PartpaymentMdOrders { get; set; }

        /// <summary>
        /// Код ответа AVS-проверки (проверка адреса и почтового индекса держателя карты)
        /// </summary>
        [JsonProperty("avsCode")]
        [JsonPropertyName("avsCode")]
        [JsonInclude]
        protected string? AVSCodeValue { get; set; }

        /// <summary>
        /// Код ответа AVS-проверки (проверка адреса и почтового индекса держателя карты)
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public AVSCode? AVSCode
        {
            get => AVSCodeValue != null ? AVSCode.Parse(AVSCodeValue) : null;
            set => AVSCodeValue = value?.Value;
        }

        /// <summary>
        /// Были ли средства принудительно возвращены клиенту банком
        /// </summary>
        [JsonProperty("chargeback")]
        [JsonPropertyName("chargeback")]
        [Newtonsoft.Json.JsonConverter(typeof(BooleanStringConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonConverter.BooleanStringConverter))]
        public bool? Chargeback { get; set; }

        /// <summary>
        /// Дата и время авторизации в формате UNIX-времени
        /// </summary>
        [JsonProperty("authDateTime")]
        [JsonPropertyName("authDateTime")]
        [JsonInclude]
        protected long? AuthTS { get; set; }

        /// <summary>
        /// Дата и время авторизации
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime? AuthDateTimeUtc => AuthTS != null
            ? DateTimeOffset.FromUnixTimeSeconds(AuthTS.Value).UtcDateTime
            : null;

        /// <summary>
        /// Номер транзакции FE.
        /// </summary>
        [JsonProperty("feUtrnno")]
        [JsonPropertyName("feUtrnno")]
        public long? FEUTransaction { get; set; }

        [JsonProperty("payerData")]
        [JsonPropertyName("payerData")]
        public PayerData? PayerData { get; set; }

        /// <summary>
        /// <para>v: 01+</para>
        /// </summary>
        [JsonProperty("secureAuthInfo")]
        [JsonPropertyName("secureAuthInfo")]
        public SecureAuthInfo? SecureAuthInfo { get; set; }

        /// <summary>
        /// Тэг с информацией о суммах подтверждения, списания, возврата
        /// <para>v: 03+</para>
        /// </summary>
        [JsonProperty("paymentAmountInfo")]
        [JsonPropertyName("paymentAmountInfo")]
        public PaymentAmountInfo? PaymentAmountInfo { get; set; }

        /// <summary>
        /// <para>Дополнительные параметры транзакции</para>
        /// <para>bindingOriginalNetRefNum — идентификатор первого платежа по созданию связки</para>
        /// <para>paymentNetRefNum — идентификатор, полученный в ходе последней оплаты по связке</para>
        /// <para>originalPaymentNetRefNum — идентификатор инициирующей транзакции по созданию связки, передаётся мерчантами, хранящих связки на своей стороне (передаётся в paymentOrder)</para>
        /// </summary>
        [JsonProperty("transactionAttributes")]
        [JsonPropertyName("transactionAttributes")]
        [Newtonsoft.Json.JsonConverter(typeof(NameValuePropertyConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonConverter.NameValuePropertyConverter))]
        public Dictionary<string, string>? TransactionAttributes { get; set; }

        /// <summary>
        /// Тэг с информацией о Банке-эмитенте
        /// <para>v: 03+</para>
        /// </summary>
        [JsonProperty("bankInfo")]
        [JsonPropertyName("bankInfo")]
        public BankInfo? BankInfo { get; set; }
        
        [JsonProperty("customerDetails")]
        [JsonPropertyName("customerDetails")]
        public CustomerDetails? CustomerDetails { get; set; }

        [JsonProperty("deliveryInfo")]
        [JsonPropertyName("deliveryInfo")]
        public DeliveryInfo? DeliveryInfo { get; set; }

        // TODO: add converters for NameParamsProperty
        //[JsonProperty("pluginInfo")]
        //[JsonPropertyName("pluginInfo")]
        //public Dictionary<string, string>? PluginInfo { get; set; }
    }
}
