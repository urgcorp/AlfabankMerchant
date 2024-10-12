using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using alfabank.Exceptions;
using alfabank.ComponentModel;

namespace alfabank.RestClient
{
    /// <summary>
    /// Client is mainly responible for making the HTTP call to the REST API backend
    /// </summary>
    public class AlfabankRestClient<TConfig> : IAlfabankClient
        where TConfig : AlfabankConfiguration
    {
        protected const string CLIENT_TYPE = "REST";

        private readonly ILogger? _logger;
        private readonly TConfig _config;

        public readonly Dictionary<string, string> DefaultHeaders = new();

        private readonly HttpClient _client;

        public string? Merchant => _config.Merchant;

        public AlfabankRestClient(TConfig config)
        {
            _config = config;

            _client = new HttpClient()
            {
                BaseAddress = new Uri(_config.BasePath)
            };
        }

        public AlfabankRestClient(ILogger<AlfabankRestClient<TConfig>> logger, TConfig config)
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

            Dictionary<string, string> queryParams = action.GetActionParams(authentication ?? new AuthParams()
            {
                Login = _config.Login,
                Password = _config.Password,
                Token = _config.Token
            });
            var content = new FormUrlEncodedContent(queryParams);

            var response = await _client.PostAsync(actionUrl, content)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);
        }

        protected static readonly Regex _errorRegex = new Regex("\"errorCode\"\\s*:\\s*\"(\\d+)\"\\s*,\\s*\"errorMessage\"\\s*:\\s*\"(.*?)\"", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary></summary>
        /// <param name="action">Request</param>
        /// <exception cref="AlfabankException"></exception>
        public async Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, AuthParams? authentication = null)
            where TResponse : class
        {
            var respJson = await CallActionRawAsync(action, authentication).ConfigureAwait(false);
            var errorMatch = _errorRegex.Match(respJson);
            if (errorMatch.Success && errorMatch.Groups[1].Value != "0")
                throw new AlfabankException(int.Parse(errorMatch.Groups[1].Value), errorMatch.Groups[2].Value);

            return JsonConvert.DeserializeObject<TResponse>(respJson)!;
        }
    }

    public class AlfabankRestClient : AlfabankRestClient<AlfabankConfiguration>
    {
        public AlfabankRestClient(AlfabankConfiguration config) : base(config)
        { }

        public AlfabankRestClient(ILogger<AlfabankRestClient<AlfabankConfiguration>> logger, AlfabankConfiguration config)
            : base(logger, config)
        { }
    }
}
