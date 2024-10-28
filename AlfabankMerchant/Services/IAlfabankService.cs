using AlfabankMerchant.Abstractions;
using AlfabankMerchant.Actions;
using AlfabankMerchant.Models;
using AlfabankMerchant.Models.Response;

namespace AlfabankMerchant.Services
{
    public interface IAlfabankService : IAlfabankMerchantService, IAlfabankOrderingService
    {
        /// <summary>
        /// Регистрация заказа с предавторизацией
        /// </summary>
        Task RegisterOrderPreAuthAsync(RegisterOrderPreAuthAction action, AuthParams? auth = null);

        /// <summary>
        /// Списание суммы предавторизации (полной или частичной)
        /// </summary>
        Task DepositOrderAsync(DepositOrderAction action, AuthParams? auth = null);

        /// <summary>
        /// Запрос статистики по платежам за период
        /// </summary>
        Task<LastOrdersForMerchants> GetLastOrdersForMerchantAsync(GetLastOrdersForMerchantsAction action, AuthParams? auth = null);

        /// <summary>
        /// Расширенный запрос состояния заказа
        /// </summary>
        Task<Order> GetOrderStatusExtendedAsync(GetOrderStatusExtendedAction action, AuthParams? auth = null);
    }

    public interface IAlfabankService<TConfig, TClient> : IAlfabankService
        where TConfig : AlfabankConfiguration
        where TClient : IAlfabankClient<TConfig>
    { }

    public interface IAlfabankService<TClient> : IAlfabankService<AlfabankConfiguration, TClient>
        where TClient : IAlfabankClient<AlfabankConfiguration>
    { }
}
