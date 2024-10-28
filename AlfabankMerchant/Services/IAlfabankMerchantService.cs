using AlfabankMerchant.Actions;
using AlfabankMerchant.Common;
using AlfabankMerchant.Models;
using AlfabankMerchant.Models.Response;
using AlfabankMerchant.Services.Context;

namespace AlfabankMerchant.Services
{
    public interface IAlfabankMerchantService : IAlfabankMerchant, IAlfabankMerchantOrderingService
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

    public interface IAlfabankService<TConfig, TClient> : IAlfabankMerchantService
        where TConfig : AlfabankConfiguration
        where TClient : IAlfabankClient<TConfig>
    { }

    public interface IAlfabankService<TClient> : IAlfabankService<AlfabankConfiguration, TClient>
        where TClient : IAlfabankClient<AlfabankConfiguration>
    { }
}
