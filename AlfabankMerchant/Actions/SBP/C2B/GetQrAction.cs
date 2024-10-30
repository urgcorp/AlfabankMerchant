using AlfabankMerchant.ComponentModel;
using AlfabankMerchant.Models;

namespace AlfabankMerchant.Actions.SBP.C2B
{
    [LoginAuthorization]
    [RestUrl("rest/sbp/c2b/qr/dynamic/get.do")]
    public class GetQrAction : AlfabankAction<QrSbp>
    {
        /// <summary>
        /// Номер заказа в системе платёжного шлюза
        /// </summary>
        [ActionProperty("mdOrder", true, Type = "ANS..36")]
        public string OrderId { get; set; }

        /// <summary>
        /// Высота QR-кода в пикселах
        /// <para> Укажите, если требуется renderedQR</para>
        /// <para>Минимальное значение: 10. Максимальное значение: 1000 </para>
        /// </summary>
        [ActionProperty("qrHeight", false, Type = "N..4")]
        public int? QrHeight { get; set; }

        /// <summary>
        /// Ширина QR-кода пикселах
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
    }
}
