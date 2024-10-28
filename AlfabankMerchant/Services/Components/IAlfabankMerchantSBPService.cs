using AlfabankMerchant.Common;

namespace AlfabankMerchant.Services.Components
{
    public interface IAlfabankMerchantSBPService : IAlfabankMerchant
    {
        /// <summary>
        /// Запрос получения QR кода по СБП
        /// </summary>
        Task GetQrCode(object action, AuthParams? authParams = null);

        /// <summary>
        /// Запрос получения статуса платежа по QR коду по СБП
        /// </summary>
        Task GetQrCodePaymentStatus(object action, AuthParams? authParams = null);
    }
}
