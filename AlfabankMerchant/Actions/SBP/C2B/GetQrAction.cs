using AlfabankMerchant.ComponentModel;
using AlfabankMerchant.Models;

namespace AlfabankMerchant.Actions.SBP.C2B
{
    [RestUrl(AlfabankRestActions.SBP_C2B.GetPaymentQr)]
    public class GetQrAction : AlfabankAction<QrSbp>
    {
        /// <summary>
        /// Номер заказа в системе платёжного шлюза
        /// </summary>
        [ActionProperty("mdOrder", true, Type = "ANS..36")]
        public string OrderId { get; set; }

        /// <summary>
        /// Счет юридического лица
        /// </summary>

        [ActionProperty("account")]
        public string? Account { get; set; }

        /// <summary>
        /// Идентификатор банка-участника SBP
        /// </summary>
        [ActionProperty("memberId")]
        public string? MemberId { get; set; }

        /// <summary>
        /// Идентификатор ТСП
        /// </summary>
        [ActionProperty("tspMerchantId")]
        public string? TspMerchantId { get; set; }

        /// <summary>
        /// <para>Дополнительная информация от TSP</para>
        /// <para>Если не заполнена, то по умолчанию будет подставлено описание заказа, если оно присутствует</para>
        /// Максимальная длина: 140 символов
        /// </summary>
        [ActionProperty("paymentPurpose", Type = "ANS..140")]
        public string? Purpose { get; set; }

        /// <summary>
        /// Высота QR-кода в пикселах
        /// <para> Укажите, если требуется renderedQR</para>
        /// <para>Минимальное значение: 10. Максимальное значение: 1000 </para>
        /// </summary>
        [ActionProperty("qrHeight", false, Type = "N..4")]
        public int? QrHeight { get; set; }

        /// <summary>
        /// Ширина QR-кода в пикселах
        /// <para> Укажите, если требуется renderedQR</para>
        /// <para>Минимальное значение: 10. Максимальное значение: 1000 </para>
        /// </summary>
        [ActionProperty("qrWidth", false, Type = "N..4")]
        public int? QrWidth { get; set; }

        /// <summary>
        /// Формат QR-кода
        /// </summary>
        [ActionProperty("qrFormat", false, Type = "ANS*")]
        public QrFormat? QrFormat { get; set; }

        /// <summary>
        /// Ссылка для автоматического возврата из приложения банка в приложение или на сайт ТСП
        /// </summary>
        [ActionProperty("redirectUrl", Type = "ANS..1024")]
        public string? RedirectUrl { get; set; }

        [ActionProperty("createSubscription")]
        private string? createSubscription => CreateSubscription.HasValue
            ? CreateSubscription.Value ? "true" : "false"
            : null;

        /// <summary>
        /// Необходима привязка счета плательщика (сохранение связки). По умолчанию: false
        /// </summary>
        public bool? CreateSubscription { get; set; } = false;

        /// <summary>
        /// Идентификатор типа предоставляемой услуги для привязки счета
        /// </summary>
        [ActionProperty("subscriptionServiceId")]
        public string? SubscriptionServiceId { get; set; }

        /// <summary>
        /// Наименование типа предоставляемой услуги для привязки счета
        /// </summary>
        [ActionProperty("subscriptionServiceName")]
        public string? SubscriptionServiceName { get; set; }
    }
}
