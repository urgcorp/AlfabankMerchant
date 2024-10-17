using alfabank.Abstractions;
using alfabank.Actions;
using alfabank.Models;
using alfabank.Models.Response;

namespace alfabank.Services
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
}
