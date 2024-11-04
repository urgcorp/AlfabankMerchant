﻿using AlfabankMerchant.Common;
using AlfabankMerchant.Models;
using AlfabankMerchant.Models.Response;
using AlfabankMerchant.Actions;

namespace AlfabankMerchant.Services
{
    /// <summary>
    /// Operations that possible with merchant token
    /// </summary>
    public interface IAlfabankMerchantTokenService : IAlfabankMerchant
    {
        /// <summary>
        /// Register new order
        /// </summary>
        Task<RegisterOrderResponse> RegisterOrderAsync(RegisterOrderAction action, AuthParams? auth = null);


        /// <summary>
        /// Расширенный запрос состояния заказа
        /// </summary>
        Task<Order> GetOrderStatusExtendedAsync(GetOrderStatusExtendedAction action, AuthParams? auth = null);
    }

    public interface IAlfabankMerchantTokenService<TConfig, TClient> : IAlfabankMerchantTokenService
        where TConfig : AlfabankConfiguration
        where TClient : IAlfabankMerchantClient<TConfig>
    { }

    public interface IAlfabankMerchantTokenService<TClient> : IAlfabankMerchantTokenService<AlfabankConfiguration, TClient>
        where TClient : IAlfabankMerchantClient<AlfabankConfiguration>
    { }
}
