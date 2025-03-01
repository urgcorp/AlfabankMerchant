﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.WSClient
{
    /// <summary>
    /// Client is mainly responible for making the HTTP call to the Web-Service (WSDL) API backend
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    public class AlfabankMerchantWSClient<TConfig> : IAlfabankMerchantClient<TConfig>, IAlfabankMerchantRawClient
        where TConfig : AlfabankConfiguration
    {
        protected const string CLIENT_TYPE = "WS";
        public const string REQUESTS_PATH = "soap/merchant-ws";

        private readonly ILogger? _logger;
        private readonly TConfig? _config;

        public readonly Dictionary<string, string> DefaultHeaders = new();

        private readonly HttpClient _client;

        public string? Merchant => _config?.Merchant;

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

        public Task<string> CallRawAsync(string actionUrl, Dictionary<string, string> queryParams, AlfabankConfiguration? configuration,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<string> CallActionRawAsync(AlfabankAction action, AlfabankConfiguration? configuration, CancellationToken cancellationToken = default)
        {
            // var actionUrl = action.FindDefaultActionUrl(CLIENT_TYPE);
            // if (string.IsNullOrEmpty(actionUrl))
            //     throw new NotImplementedException("Unable to determine action URL to call for");

            // _logger?.LogTrace("Calling \"{action}\"", actionUrl);
            throw new NotImplementedException();
        }

        public Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, AlfabankConfiguration configuration,
            CancellationToken cancellationToken = default) where TResponse : class
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, TConfig? configuration,
            CancellationToken cancellationToken = default) where TResponse : class
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, CancellationToken cancellationToken = default) where TResponse : class
        {
            throw new NotImplementedException();
        }
    }
}
