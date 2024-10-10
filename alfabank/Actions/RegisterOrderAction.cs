using alfabank.ComponentModel;
using alfabank.Models.Response;

namespace alfabank.Actions
{
    /// <summary>
    /// Запрос регистрации заказа
    /// </summary>
    public sealed class RegisterOrderAction : AlfabankAction<RegisterOrderResponse>
    {
        public override string Action { get; set; } = AlfabankRestActions.RegisterOrder;

        /// <summary>
        /// Номер (идентификатор) заказа в системе магазина, уникален для каждого магазина в пределах системы
        /// </summary>
        [ActionProperty("orderNumber", true, Type = "AN..32")]
        public string OrderNumber { get; set; }

        /// <summary>
        /// Сумма платежа в копейках (или центах)
        /// </summary>
        [ActionProperty("amount", true, Type = "N..12")]
        public int? Amount { get; set; }

        /// <summary>
        /// Код валюты платежа ISO 4217.
        /// <para>Если не указан, считается равным 810 (российские рубли)</para>
        /// </summary>
        [ActionProperty("currency", Type = "N3")]
        public string? CurrencyCode { get; set; }

        /// <summary>
        /// Адрес, на который требуется перенаправить пользователя в случае успешной оплаты.
        /// <para></para>Адрес должен быть указан полностью, включая используемый протокол (например, https://test.ru вместо test.ru). В противном случае пользователь будет перенаправлен по адресу следующего вида: http://адрес_платёжного_шлюза/адрес_продавца
        /// </summary>
        [ActionProperty("returnUrl", true, Type = "AN..512")]
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Адрес, на который требуется перенаправить пользователя в случае неуспешной оплаты. 
        /// <para></para>Адрес должен быть указан полностью, включая используемый протокол (например, https://test.ru вместо test.ru). В противном случае пользователь будет перенаправлен по адресу следующего вида: http://адрес_платёжного_шлюза/адрес_продавца
        /// </summary>
        [ActionProperty("failUrl", Type = "AN..512")]
        public string? FailUrl { get; set; }

        /// <summary>
        /// <para>Параметр позволяет воспользоваться функциональностью динамической отправки callback-уведомлений. В нем можно передать адрес, на который будут отправляться все "платежные" callback-уведомления, активированные для мерчанта</para>
        /// <para></para>
        /// Под платежными понимаются callback-уведомления о следующих событиях: успешный холд, платеж отклонен по таймауту, успешное списание, возврат, отмена. При этом активированные для мерчанта callback-уведомления, не относящиеся к платежам (включение/выключение связки, создание связки), будут отправляться на статический адрес для callback-ов.
        /// <para>Необходимо обратиться в тех поддержку для настройки</para>
        /// </summary>
        [ActionProperty("dynamicCallbackUrl", Type = "AN..512")]
        public string? DynamicCallbackUrl { get; set; }

        /// <summary>
        /// Описание заказа в свободной форме
        /// </summary>
        [ActionProperty("description", Type = "AN..512")]
        public string? Description { get; set; }

        /// <summary>
        /// Язык в кодировке ISO 639-1.
        /// <para>Если не указан, будет использован язык, указанный в настройках магазина как язык по умолчанию</para>
        /// </summary>
        [ActionProperty("language", Type = "A2")]
        public string? Language { get; set; }

        /// <summary>
        ///  По значению данного параметра определяется, какие страницы платёжного интерфейса должны загружаться для клиента
        /// <para>Стандартные значения: DESKTOP, MOBILE</para>
        /// <para></para>Если магазин создал страницы платёжного интерфейса, добавив в название файлов страниц произвольные префиксы, передайте значение нужного префикса в параметре pageView для загрузки соответствующей страницы
        /// <para>В архиве страниц платёжного интерфейса будет осуществляться поиск страниц с названиями PREFIX_payment_LOCALE.html и PREFIX_error_LOCALE.html</para>
        /// <para>Если параметр отсутствует, либо не соответствует формату, то по умолчанию считается DESKTOP</para>
        /// </summary>
        [ActionProperty("pageView", Type = "ANS..20")]
        public string? PageViewPrefix { get; set; }

        /// <summary>
        /// Номер (идентификатор) клиента в системе магазина. Используется для реализации функционала связок.
        /// <para>Может присутствовать, если магазину разрешено создание связок.</para>
        /// <para>Указание этого параметра при платежах по связке необходимо - в противном случае платёж будет неуспешен.</para>
        /// </summary>
        [ActionProperty("clientId", Type = "AN..255")]
        public string? ClientId { get; set; }

        /// <summary>
        /// Чтобы зарегистрировать заказ от имени дочернего мерчанта, укажите его логин в этом параметре.
        /// </summary>
        [ActionProperty("merchantLogin", Type = "AN..255")]
        public string? MerchantLogin { get; set; }

        /// <summary>
        /// <para>Дополнительные параметры мерчанта</para>
        /// Данные поля могут быть переданы в процессинг банка для последующего отображения в реестрах.
        /// Включение данной функциональности возможно по согласованию с банком в период интеграции.
        /// <para></para>
        /// Если для продавца настроена отправка уведомлений покупателю, адрес электронной почты покупателя должен передаваться в этом блоке в параметре с именем email
        /// </summary>
        public Dictionary<string, string>? MerchantParams { get; set; }

        /// <summary>
        /// JSON string <see cref="MerchantParams"/>
        /// </summary>
        [ActionProperty("jsonParams", Type = "AN..1024")]
        private string? MerchantParamsJson => GetJsonParamsString(MerchantParams);

        /// <summary>
        /// IP-адрес покупателя
        /// </summary>
        [ActionProperty("ip", Type = "ANS..39")]
        public string? BuyerIpAddress { get; set; }

        /// <summary>
        /// <para>Продолжительность жизни заказа в секундах</para>
        /// <para>В случае если параметр не задан, будет использовано значение, указанное в настройках мерчанта или время по умолчанию (1200 секунд = 20 минут)</para>
        /// <para>Если в запросе присутствует параметр <see cref="ExpirationDate"/>, то значение параметра <see cref="SessionTimeoutSec"/> не учитывается</para>
        /// </summary>
        [ActionProperty("sessionTimeoutSecs", Type = "N...9")]
        public int? SessionTimeoutSec { get; set; }

        /// <summary>
        /// <para>Дата и время окончания жизни заказа</para>
        /// <para>Если этот параметр не передаётся в запросе, то для определения времени окончания жизни заказа используется <see cref="SessionTimeoutSec"/></para>
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// <see cref="ExpirationDate"/>
        /// <para>Формат: yyyy-MM-ddTHH:mm:ss</para>
        /// </summary>
        [ActionProperty("expirationDate", Type = "ANS")]
        private string? ExpirationDateParam => ExpirationDate?.ToString("yyyy-MM-ddTHH:mm:ss");

        /// <summary>
        /// <para>Идентификатор связки, созданной ранее</para>
        /// <para>Может использоваться, только если у магазина есть разрешение на работу со связками. Если этот параметр передаётся в данном запросе, то это означает:</para>
        /// <para>1. Данный заказ может быть оплачен только с помощью связки</para>
        /// <para>2. Плательщик будет перенаправлен на платёжную страницу, где требуется только ввод CVC</para>
        /// </summary>
        [ActionProperty("bindingId", Type = "AN..255")]
        public string? BindingId { get; set; }

        /// <summary>
        /// <para>AUTO_PAYMENT - Если запрос на регистрацию заказа инициирует проведение автоплатежей</para>
        /// <para>VERIFY - Если указать это значение после запроса на регистрацию заказа произойдёт верификация держателя карты без списания средств с его счёта, поэтому в запросе можно передавать нулевую сумму</para>
        /// </summary>
        [ActionProperty("features", Type = "ANS..255")]
        public string? Features { get; set; }

        /// <summary>
        /// Электронная почта покупателя
        /// </summary>
        [ActionProperty("email", Type = "ANS..40")]
        public string? Email { get; set; }

        /// <summary>
        /// <para>Номер телефона покупателя</para>
        /// Если в телефон включён код страны, номер должен начинаться со знака плюс («+»).
        /// Если телефон передаётся без знака плюс («+»), то код страны указывать не следует.
        /// Таким образом, допустимы следующие варианты: +79998887766; 9998887766
        /// </summary>
        [ActionProperty("phone", Type = "ANS.12")]
        public string? Phone { get; set; }
    }
}
