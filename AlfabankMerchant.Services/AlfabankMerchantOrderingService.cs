using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using AlfabankMerchant.Actions;
using AlfabankMerchant.Models.Response;
using AlfabankMerchant.Common;
using AlfabankMerchant.Services.Components;
using AlfabankMerchant.Models;

namespace AlfabankMerchant.Services
{
    public class AlfabankMerchantOrderingService<TConfig, TClient> : IAlfabankOrderingService<TConfig, TClient>
        where TConfig : AlfabankConfiguration
        where TClient : IAlfabankMerchantClient<TConfig>
    {
        private readonly ILogger? _logger;
        private readonly TClient _client;

        public string? Merchant => _client.Merchant;

        public AlfabankMerchantOrderingService(ILogger<AlfabankMerchantOrderingService<TConfig, TClient>> logger, TClient client)
        {
            _logger = logger;
            _client = client;
        }

        public AlfabankMerchantOrderingService(TClient client)
        {
            _client = client;
        }

        public Task<RegisterOrderResponse> RegisterOrderAsync(RegisterOrderAction action, AuthParams? auth = null)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (!action.ValidateActionParams(out var errors))
            {
                string errorsList = string.Join("\n", errors.Select(x => $"    {x.Key} - {x.Value}"));
                _logger?.LogDebug($"Action parameters validation failed:\n{errorsList}");
                throw new ArgumentException($"One ore multiple action parameters validation failed\n{errorsList}", nameof(action));
            }

            return _client.CallActionAsync(action, auth);
        }

        public Task<RegisterOrderResponse> RegisterOrderAsync(string orderNumber, Currency currency, int amount, string returnUrl, string? description, AuthParams? auth = null,
            string? email = null, string? phone = null, string? clientId = null, string? clientIp = null)
        {
            var action = new RegisterOrderAction()
            {
                OrderNumber = orderNumber,
                Currency = currency,
                Amount = amount,
                ReturnUrl = returnUrl,
                Email = email,
                Phone = phone,
                Description = description,
                ClientId = clientId,
                ClientIpAddress = clientIp
            };
            return RegisterOrderAsync(action, auth);
        }

        public Task RegisterOrderPreAuthAsync(RegisterOrderPreAuthAction action, AuthParams? auth = null)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderStatusAsync(GetOrderStatusAction action, AuthParams? auth = null)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderStatusExtendedAsync(GetOrderStatusExtendedAction action, AuthParams? auth = null)
        {
            throw new NotImplementedException();
        }

        public Task<LastOrdersForMerchants> GetLastOrdersForMerchantAsync(GetLastOrdersForMerchantsAction action, AuthParams? auth = null)
        {
            throw new NotImplementedException();
        }

        public Task ReverseOrderAsync(object action, AuthParams? auth = null)
        {
            throw new NotImplementedException();
        }

        public Task DepositOrderAsync(DepositOrderAction action, AuthParams? auth = null)
        {
            throw new NotImplementedException();
        }

        public Task RefundOrderAsync(object action, AuthParams? auth = null)
        {
            throw new NotImplementedException();
        }

        public Task AddOrderParamsAsync(object action, AuthParams? auth = null)
        {
            throw new NotImplementedException();
        }
    }

    public class AlfabankOrderingService<TClient> : AlfabankMerchantOrderingService<AlfabankConfiguration, TClient>, IAlfabankOrderingService<TClient>
        where TClient : IAlfabankMerchantClient<AlfabankConfiguration>
    {
        public AlfabankOrderingService(TClient client) : base(client)
        { }

        public AlfabankOrderingService(ILogger<AlfabankMerchantOrderingService<AlfabankConfiguration, TClient>> logger, TClient client) : base(logger, client)
        { }
    }
}
