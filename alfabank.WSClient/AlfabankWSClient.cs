using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using alfabank.Exceptions;
using alfabank.ComponentModel;

namespace alfabank.WSClient
{
    /// <summary>
    /// Client is mainly responible for making the HTTP call to the Web-Service (WSDL) API backend
    /// </summary>
    /// <typeparam name="TConfig"></typeparam>
    public class AlfabankWSClient<TConfig> : IAlfabankClient
        where TConfig : AlfaBankConfiguration
    {
        private readonly ILogger? _logger;
        private readonly TConfig _config;

        public readonly Dictionary<string, string> DefaultHeaders = new();

        private readonly HttpClient _client;

        public AlfabankWSClient(TConfig config)
        {
            _config = config;

            _client = new HttpClient()
            {
                BaseAddress = new Uri(_config.BasePath)
            };
        }

        public AlfabankWSClient(ILogger<AlfabankWSClient<TConfig>> logger, TConfig config)
        {
            _logger = logger;
            _config = config;

            _client = new HttpClient()
            {
                BaseAddress = new Uri(_config.BasePath)
            };
        }

        public Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action) where TResponse : class
        {
            throw new NotImplementedException();
        }

        public Task<string> CallActionRawAsync(AlfabankAction action)
        {
            throw new NotImplementedException();
        }
    }
}
