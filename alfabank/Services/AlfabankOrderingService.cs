using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using alfabank.Actions;
using alfabank.Models.Response;

namespace alfabank.Services
{
    public class AlfabankOrderingService<TConfig, TClient> : IAlfabankOrderingService<TConfig, TClient>
        where TConfig : AlfabankConfiguration
        where TClient : IAlfabankClient<TConfig>
    {
        private readonly ILogger? _logger;
        private readonly TClient _client;

        public string? Merchant => _client.Merchant;

        public AlfabankOrderingService(ILogger<AlfabankOrderingService<TConfig, TClient>> logger, TClient client)
        {
            _logger = logger;
            _client = client;
        }

        public AlfabankOrderingService(TClient client)
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
    }

    public class AlfabankOrderingService<TClient> : AlfabankOrderingService<AlfabankConfiguration, TClient>, IAlfabankOrderingService<TClient>
        where TClient : IAlfabankClient<AlfabankConfiguration>
    {
        public AlfabankOrderingService(TClient client) : base(client)
        { }

        public AlfabankOrderingService(ILogger<AlfabankOrderingService<AlfabankConfiguration, TClient>> logger, TClient client) : base(logger, client)
        { }
    }

}
