using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PuppeteerSharp;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.BrowserClient
{
    public class AlfabankPuppeteerClient<TConfig> : IAlfabankClient<TConfig>
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

        public Task<string> CallActionRawAsync(AlfabankAction action, AuthParams? authentication = null)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, AuthParams? authentication = null)
            where TResponse : class
        {
            throw new NotImplementedException();
        }
    }
}
