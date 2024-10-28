using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using AlfabankMerchant.Exceptions;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.WSClient
{
    /// <summary>
    /// Client is mainly responible for making the HTTP call to the Web-Service (WSDL) API backend
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    public class AlfabankMerchantWSClient<TConfig> : IAlfabankMerchantClient<TConfig>
        where TConfig : AlfabankConfiguration
    {
        protected const string CLIENT_TYPE = "WS";
        public const string REQUESTS_PATH = "soap/merchant-ws";

        private readonly ILogger? _logger;
        private readonly TConfig _config;

        public readonly Dictionary<string, string> DefaultHeaders = new();

        private readonly HttpClient _client;

        public string? Merchant => _config.Merchant;

        public AlfabankMerchantWSClient(TConfig config)
        {
            _config = config;

            _client = new HttpClient()
            {
                BaseAddress = new Uri(_config.BasePath)
            };
        }

        public AlfabankMerchantWSClient(ILogger<AlfabankMerchantWSClient<TConfig>> logger, TConfig config)
        {
            _logger = logger;
            _config = config;

            _client = new HttpClient()
            {
                BaseAddress = new Uri(_config.BasePath)
            };
        }

        public async Task<string> CallActionRawAsync(AlfabankAction action, AuthParams? authentication = null)
        {
            var actionUrl = action.FindDefaultActionUrl(CLIENT_TYPE);
            if (string.IsNullOrEmpty(actionUrl))
                throw new NotImplementedException("Unable to determine action URL to call for");

            _logger?.LogTrace("Calling \"{action}\"", actionUrl);
            throw new NotImplementedException();
        }

        public async Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, AuthParams? authentication = null)
            where TResponse : class
        {
            var respBody = await CallActionAsync(action, authentication)
                .ConfigureAwait(false);

            throw new NotImplementedException();
        }
    }
}
