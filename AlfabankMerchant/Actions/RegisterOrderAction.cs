using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using AlfabankMerchant.Common;
using AlfabankMerchant.ComponentModel;
using AlfabankMerchant.Models.Response;

namespace AlfabankMerchant.Actions
{
    /// <summary>
    /// Запрос регистрации заказа
    /// </summary>
    [RestUrl(AlfabankRestActions.RegisterOrder)]
    public sealed class RegisterOrderAction : AlfabankAction<RegisterOrderResponse>
    {
        /// <summary>
        /// Номер (идентификатор) заказа в системе магазина, уникален для каждого магазина в пределах системы
        /// </summary>
        [ActionProperty("orderNumber", true, Type = "AN..32")]
        [JsonPropertyName("orderNumber")]
        public string? OrderNumber { get; set; }

        /// <summary>
        /// Валюта платежа
        /// <para>Если не указана - российский рубль (RUR)</para>
        /// </summary>
        [JsonPropertyName("currency")]
        public Currency? Currency { get; set; }

        /// <summary>
        /// Сумма платежа в копейках (или центах)
        /// </summary>
        [ActionProperty("amount", true, Type = "N..12")]
        [JsonPropertyName("amount")]
        public int? Amount { get; set; }

        /// <summary>
        /// Код валюты платежа ISO 4217.
        /// <para>Если не указан, считается равным 810 (российские рубли)</para>
        /// </summary>
        [ActionProperty("currency", Type = "N3")]
        [JsonIgnore]
        private string? CurrencyCode => Currency?.CurrencyCode.ToString();

        /// <summary>
        /// <para>Адрес, на который требуется перенаправить пользователя в случае успешной оплаты.</para>
        /// <para>Адрес должен быть указан полностью, включая используемый протокол (например, https://test.ru вместо test.ru).
        /// В противном случае пользователь будет перенаправлен по адресу следующего вида: http://адрес_платёжного_шлюза/адрес_продавца</para>
        /// </summary>
        [ActionProperty("returnUrl", true, Type = "AN..512")]
        [JsonPropertyName("returnUrl")]
        public string? ReturnUrl { get; set; }

        /// <summary>
        /// <para>Адрес, на который требуется перенаправить пользователя в случае неуспешной оплаты.</para> 
        /// <para>Адрес должен быть указан полностью, включая используемый протокол (например, https://test.ru вместо test.ru).
        /// В противном случае пользователь будет перенаправлен по адресу следующего вида: http://адрес_платёжного_шлюза/адрес_продавца</para>
        /// </summary>
        [ActionProperty("failUrl", Type = "AN..512")]
        [JsonPropertyName("failUrl")]
        public string? FailUrl { get; set; }

        /// <summary>
        /// <para>Параметр позволяет воспользоваться функциональностью динамической отправки callback-уведомлений. В нем можно передать адрес, на который будут отправляться все "платежные" callback-уведомления, активированные для мерчанта</para>
        /// <para></para>
        /// Под платежными понимаются callback-уведомления о следующих событиях: успешный холд, платеж отклонен по таймауту, успешное списание, возврат, отмена. При этом активированные для мерчанта callback-уведомления, не относящиеся к платежам (включение/выключение связки, создание связки), будут отправляться на статический адрес для callback-ов.
        /// <para>Необходимо обратиться в техподдержку для настройки</para>
        /// </summary>
        [ActionProperty("dynamicCallbackUrl", Type = "AN..512")]
        [JsonPropertyName("dynamicCallbackUrl")]
        public string? DynamicCallbackUrl { get; set; }

        /// <summary>
        /// Описание заказа в свободной форме
        /// </summary>
        [ActionProperty("description", Type = "AN..512")]
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Язык в кодировке ISO 639-1.
        /// <para>Если не указан, будет использован язык, указанный в настройках магазина как язык по умолчанию</para>
        /// </summary>
        [ActionProperty("language", Type = "A2")]
        [JsonPropertyName("language")]
        public string? Language { get; set; }

        /// <summary>
        ///  По значению данного параметра определяется, какие страницы платёжного интерфейса должны загружаться для клиента
        /// <para>Стандартные значения: DESKTOP, MOBILE</para>
        /// <para></para>Если магазин создал страницы платёжного интерфейса, добавив в название файлов страниц произвольные префиксы, передайте значение нужного префикса в параметре pageView для загрузки соответствующей страницы
        /// <para>В архиве страниц платёжного интерфейса будет осуществляться поиск страниц с названиями PREFIX_payment_LOCALE.html и PREFIX_error_LOCALE.html</para>
        /// <para>Если параметр отсутствует, либо не соответствует формату, то по умолчанию считается DESKTOP</para>
        /// </summary>
        [ActionProperty("pageView", Type = "ANS..20")]
        [JsonPropertyName("pageView")]
        public string? PageViewPrefix { get; set; }

        /// <summary>
        /// Номер (идентификатор) клиента в системе магазина. Используется для реализации функционала связок.
        /// <para>Может присутствовать, если магазину разрешено создание связок.</para>
        /// <para>Указание этого параметра при платежах по связке необходимо - в противном случае платёж будет неуспешен.</para>
        /// </summary>
        [ActionProperty("clientId", Type = "AN..255")]
        [JsonPropertyName("clientId")]
        public string? ClientId { get; set; }

        /// <summary>
        /// Чтобы зарегистрировать заказ от имени дочернего мерчанта, укажите его логин в этом параметре.
        /// </summary>
        [ActionProperty("merchantLogin", Type = "AN..255")]
        [JsonPropertyName("merchantLogin")]
        public string? MerchantLogin { get; set; }

        /// <summary>
        /// <para>Дополнительные параметры мерчанта</para>
        /// Данные поля могут быть переданы в процессинг банка для последующего отображения в реестрах.
        /// Включение данной функциональности возможно по согласованию с банком в период интеграции.
        /// <para></para>
        /// Если для продавца настроена отправка уведомлений покупателю, адрес электронной почты покупателя должен передаваться в этом блоке в параметре с именем email
        /// </summary>
        [JsonPropertyName("jsonParams")]
        public Dictionary<string, string>? MerchantParams { get; set; }

        /// <summary>
        /// JSON string <see cref="MerchantParams"/>
        /// </summary>
        [ActionProperty("jsonParams", Type = "AN..1024")]
        [JsonIgnore]
        private string? MerchantParamsJson => FormatJsonParamsString(MerchantParams);

        /// <summary>
        /// IP-адрес покупателя
        /// </summary>
        [ActionProperty("ip", Type = "ANS..39")]
        [JsonPropertyName("ip")]
        public string? ClientIpAddress { get; set; }

        /// <summary>
        /// <para>Продолжительность жизни заказа в секундах</para>
        /// <para>В случае если параметр не задан, будет использовано значение, указанное в настройках мерчанта или время по умолчанию (1200 секунд = 20 минут)</para>
        /// <para>Если в запросе присутствует параметр <see cref="ExpirationDate"/>, то значение параметра <see cref="SessionTimeoutSec"/> не учитывается</para>
        /// </summary>
        [ActionProperty("sessionTimeoutSecs", Type = "N...9")]
        [JsonPropertyName("sessionTimeoutSecs")]
        public int? SessionTimeoutSec { get; set; }

        /// <summary>
        /// <para>Дата и время окончания жизни заказа</para>
        /// <para>Если этот параметр не передаётся в запросе, то для определения времени окончания жизни заказа используется <see cref="SessionTimeoutSec"/></para>
        /// </summary>
        [JsonPropertyName("expirationDate")]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// <see cref="ExpirationDate"/>
        /// <para>Формат: yyyy-MM-ddTHH:mm:ss</para>
        /// </summary>
        [ActionProperty("expirationDate", Type = "ANS")]
        [JsonIgnore]
        private string? ExpirationDateParam => ExpirationDate?.ToString("yyyy-MM-ddTHH:mm:ss");

        /// <summary>
        /// <para>Время автозавершения заказа</para>
        /// Если заказ не был завершен к указанному времени, то он завершится автоматически
        /// </summary>
        [JsonPropertyName("autocompletionDate")]
        public DateTime? AutocompletionDate { get; set; }

        /// <summary>
        /// <see cref="AutocompletionDate"/>
        /// <para>Формат: yyyy-MM-ddTHH:mm:ss</para>
        /// </summary>
        [ActionProperty("autocompletionDate", Type = "ANS")]
        [JsonIgnore]
        private string? AutocompletionDateParam => AutocompletionDate?.ToString("yyyy-MM-ddTHH:mm:ss");

        /// <summary>
        /// <para>Идентификатор связки, созданной ранее.</para>
        /// <para>Может использоваться, только если у магазина есть разрешение на работу со связками.
        /// Если этот параметр передаётся в данном запросе, то это означает:</para>
        /// <para>1. Данный заказ может быть оплачен только с помощью связки</para>
        /// <para>2. Плательщик будет перенаправлен на платёжную страницу, где требуется только ввод CVC</para>
        /// </summary>
        [ActionProperty("bindingId", Type = "AN..255")]
        [JsonPropertyName("bindingId")]
        public string? BindingId { get; set; }

        /// <summary>
        /// Имя владельца карты
        /// </summary>
        [ActionProperty("cardholderName")]
        [JsonPropertyName("cardholderName")]
        public string? CardholderName { get; set; }

        /// <summary>
        /// <para>AUTO_PAYMENT - Если запрос на регистрацию заказа инициирует проведение автоплатежей</para>
        /// <para>VERIFY - Если указать это значение после запроса на регистрацию заказа произойдёт верификация держателя карты без списания средств с его счёта, поэтому в запросе можно передавать нулевую сумму</para>
        /// </summary>
        [ActionProperty("features", Type = "ANS..255")]
        [JsonPropertyName("features")]
        public string? Features { get; set; }

        /// <summary>
        /// Номер заказа в платежной системе с типом расчета Предоплата или Аванс
        /// </summary>
        [ActionProperty("prepaymentMdOrder")]
        [JsonPropertyName("prepaymentMdOrder")]
        public string? PrepaymentOrderId { get; set; }

        /// <summary>
        /// Размер комиссии мерчанта в минимальных единицах валюты
        /// <para>Параметр передается только при включении соответствующей пермиссии: Разрешена передача комиссии Мерчанта.</para>
        /// </summary>
        [ActionProperty("feeInput")]
        [JsonPropertyName("feeInput")]
        public int? Fee { get; set; }

        /// <summary>
        /// Электронная почта покупателя
        /// </summary>
        [ActionProperty("email", Type = "ANS..40")]
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        /// <para>Номер телефона покупателя</para>
        /// Если в телефон включён код страны, номер должен начинаться со знака плюс («+»).
        /// Если телефон передаётся без знака плюс («+»), то код страны указывать не следует.
        /// Таким образом, допустимы следующие варианты: +79998887766; 9998887766
        /// </summary>
        [ActionProperty("phone", Type = "ANS.12")]
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        /// <summary>
        /// Адрес доставки
        /// </summary>
        [ActionProperty("postAddress", Type = "ANS…598")]
        [JsonPropertyName("postAddress")]
        public string? PostAddress { get; set; }
    }
}
