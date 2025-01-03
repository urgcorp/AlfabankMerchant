﻿using AlfabankMerchant.Actions;
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
        Task RegisterOrderPreAuthAsync(RegisterOrderPreAuthAction action, AlfabankConfiguration? configuration = null);

        /// <summary>
        /// Register new order
        /// </summary>
        Task<RegisterOrderResponse> RegisterOrderAsync(RegisterOrderAction action, AlfabankConfiguration? configuration = null);

        /// <summary>
        /// Запрос состояния заказа
        /// </summary>
        Task<Order> GetOrderStatusAsync(GetOrderStatusAction action, AlfabankConfiguration? configuration = null);

        /// <summary>
        /// Расширенный запрос состояния заказа
        /// </summary>
        Task<Order> GetOrderStatusExtendedAsync(GetOrderStatusExtendedAction action, AlfabankConfiguration? configuration = null);

        /// <summary>
        /// Запрос статистики по платежам за период
        /// </summary>
        Task<LastOrdersForMerchants> GetLastOrdersForMerchantAsync(GetLastOrdersForMerchantsAction action, AlfabankConfiguration? configuration = null);

        /// <summary>
        /// Запрос отмены оплаты заказа
        /// </summary>
        Task ReverseOrderAsync(object action, AlfabankConfiguration? configuration = null);

        /// <summary>
        /// <para>Запрос завершения оплаты заказа</para>
        /// Списание суммы предавторизации (полной или частичной)
        /// </summary>
        Task DepositOrderAsync(DepositOrderAction action, AlfabankConfiguration? configuration = null);

        /// <summary>
        /// Запрос возврата средств оплаты заказа
        /// </summary>
        Task RefundOrderAsync(object action, AlfabankConfiguration? configuration = null);

        /// <summary>
        /// Запрос добавления дополнительных параметров к заказу
        /// </summary>
        Task AddOrderParamsAsync(object action, AlfabankConfiguration? configuration = null);
    }
}
