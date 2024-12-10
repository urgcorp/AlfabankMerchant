using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.BrowserClient
{
    public class AlfabankPuppeteerClient<TConfig> : IAlfabankMerchantClient<TConfig>
        where TConfig : AlfabankConfiguration
    {
        protected const string CLIENT_TYPE = "BROWSER";

        protected readonly ILogger? _logger;
        protected readonly TConfig _config;
        protected readonly IServiceProvider _services;
        
        public string? Merchant => _config.Merchant;

        protected readonly IBrowserProvider _browserProvider;

        public AlfabankPuppeteerClient(ILogger<AlfabankPuppeteerClient<TConfig>> logger, TConfig config, IServiceProvider services)
        {
            _logger = logger;
            _config = config;
            _services = services;

            _browserProvider = _services.GetService<IBrowserProvider>() ?? throw new NotImplementedException("TODO: create default provider");
        }

        public Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, TConfig? configuration, CancellationToken cancellationToken = default) where TResponse : class
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, CancellationToken cancellationToken = default) where TResponse : class
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, AlfabankConfiguration configuration, CancellationToken cancellationToken = default) where TResponse : class
        {
            throw new NotImplementedException();
        }
    }
}
