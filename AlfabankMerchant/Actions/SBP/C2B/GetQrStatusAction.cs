using AlfabankMerchant.ComponentModel;
using AlfabankMerchant.Models.Response;

namespace AlfabankMerchant.Actions.SBP.C2B
{
    /// <summary>
    /// Получение состояния запроса в СБП
    /// </summary>
    [LoginAuthorization]
    [RestUrl("rest/sbp/c2b/qr/dynamic/status.do")]
    public class GetQrStatusAction : AlfabankAction<SbpQrStatusResponse>
    {
        /// <summary>
        /// Номер заказа в системе платёжного шлюза
        /// </summary>
        [ActionProperty("mdOrder", true, Type = "ANS..36")]
        public string OrderId { get; set; }

        /// <summary>
        /// Идентификатор QR-кода
        /// </summary>
        [ActionProperty("qrId", true)]
        public string QrId { get; set; }
    }
}
