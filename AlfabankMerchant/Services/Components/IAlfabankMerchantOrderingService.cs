using AlfabankMerchant.Actions;
using AlfabankMerchant.Common;
using AlfabankMerchant.Models;
using AlfabankMerchant.Models.Response;

namespace AlfabankMerchant.Services.Components
{
    /// <summary>
    /// Service that provides ability to register new orders
    /// <para>Only method that can be called without permissin (Requires merchant token)</para>
    /// </summary>
    public interface IAlfabankMerchantOrderingService : IAlfabankMerchant
    {
        /// <summary>
        /// Регистрация заказа с предавторизацией
        /// </summary>
        Task RegisterOrderPreAuthAsync(RegisterOrderPreAuthAction action, AuthParams? auth = null);

        /// <summary>
        /// Register new order
        /// </summary>
        Task<RegisterOrderResponse> RegisterOrderAsync(RegisterOrderAction action, AuthParams? auth = null);

        /// <summary>
        /// Register new order
        /// </summary>
        /// <param name="orderNumber">Order number within the shop</param>
        /// <param name="currency">Payment currency</param>
        /// <param name="amount">Payment amount in cents or kopecks</param>
        /// <param name="returnUrl">Address where client will be redirected after successfull payment (like https://example.com/payment_success)</param>
        /// <param name="description">Free form order description</param>
        /// <param name="email">Payer email</param>
        /// <param name="phone">Payer phone number</param>
        /// <param name="clientId">Client ID within shop system</param>
        /// <param name="clientIp">Client IP address</param>
        /// <param name="auth">Override authentication params</param>
        Task<RegisterOrderResponse> RegisterOrderAsync(string orderNumber, Currency currency, int amount, string returnUrl, string? description, AuthParams? auth = null,
            string? email = null, string? phone = null, string? clientId = null, string? clientIp = null);

        /// <summary>
        /// Запрос состояния заказа
        /// </summary>
        Task<Order> GetOrderStatusAsync(GetOrderStatusAction action, AuthParams? auth = null);

        /// <summary>
        /// Расширенный запрос состояния заказа
        /// </summary>
        Task<Order> GetOrderStatusExtendedAsync(GetOrderStatusExtendedAction action, AuthParams? auth = null);

        /// <summary>
        /// Запрос статистики по платежам за период
        /// </summary>
        Task<LastOrdersForMerchants> GetLastOrdersForMerchantAsync(GetLastOrdersForMerchantsAction action, AuthParams? auth = null);

        /// <summary>
        /// Запрос отмены оплаты заказа
        /// </summary>
        Task ReverseOrderAsync(object action, AuthParams? auth = null);

        /// <summary>
        /// <para>Запрос завершения оплаты заказа</para>
        /// Списание суммы предавторизации (полной или частичной)
        /// </summary>
        Task DepositOrderAsync(DepositOrderAction action, AuthParams? auth = null);

        /// <summary>
        /// Запрос возврата средств оплаты заказа
        /// </summary>
        Task RefundOrderAsync(object action, AuthParams? auth = null);

        /// <summary>
        /// Запрос добавления дополнительных параметров к заказу
        /// </summary>
        Task AddOrderParamsAsync(object action, AuthParams? auth = null);
    }

    public interface IAlfabankOrderingService<TConfig, TClient> : IAlfabankMerchantOrderingService
        where TConfig : AlfabankConfiguration
        where TClient : IAlfabankClient<TConfig>
    { }

    public interface IAlfabankOrderingService<TClient> : IAlfabankOrderingService<AlfabankConfiguration, TClient>
        where TClient : IAlfabankClient<AlfabankConfiguration>
    { }
}
