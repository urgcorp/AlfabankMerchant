using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using alfabank.Actions;
using alfabank.Models.Response;

namespace alfabank.Services
{
    public class AlfabankOrderingService<TConfig, TClient> : IAlfabankOrderingService
        where TConfig : AlfabankConfiguration
        where TClient : IAlfabankClient<TConfig>
    {
        private readonly ILogger? _logger;
        private readonly TClient _client;

        public string? Merchant => _client.Merchant;

        public AlfabankOrderingService(ILogger<TClient> logger, TClient client)
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
            throw new NotImplementedException();
        }

        public Task<RegisterOrderResponse> RegisterOrderAsync(string orderNumber, Currency currency, int amount, string returnUrl, string? description, string? email = null, string? phone = null, string? clientId = null, string? clientIp = null, AuthParams? auth = null)
        {
            throw new NotImplementedException();
        }
    }

    public class AlfabankOrderingService<TClient> : AlfabankOrderingService<AlfabankConfiguration, TClient>
        where TClient : IAlfabankClient<AlfabankConfiguration>
    {
        public AlfabankOrderingService(TClient client) : base(client)
        { }

        public AlfabankOrderingService(ILogger<TClient> logger, TClient client) : base(logger, client)
        { }
    }

}
