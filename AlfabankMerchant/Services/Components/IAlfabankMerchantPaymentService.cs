using AlfabankMerchant.Common;

namespace AlfabankMerchant.Services.Components
{
    public interface IAlfabankMerchantPaymentService : IAlfabankMerchant
    {
        /// <summary>
        /// Запрос проверки вовлеченности карты в 3DS
        /// </summary>
        Task VerifyEnrollment(object action, AlfabankConfiguration? configuration = null);

        /// <summary>
        /// Запрос проведения платежа по связке
        /// </summary>
        Task PaymentOrderBinding(object action, AlfabankConfiguration? configuration = null);

        /// <summary>
        /// Запрос на регулярный платёж
        /// </summary>
        Task RecurrentPayment(object action, AlfabankConfiguration? configuration = null);

        /// <summary>
        /// Запрос на оплату через внешнюю платёжную систему
        /// </summary>
        Task PaymentOtherWay(object action, AlfabankConfiguration? configuration = null);
    }
}
