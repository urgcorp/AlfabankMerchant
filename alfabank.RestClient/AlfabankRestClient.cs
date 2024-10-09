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
    /// API client is mainly responible for making the HTTP call to the API backend.
    /// </summary>
    public class AlfabankRestClient<TConfig>
        where TConfig : AlfaBankConfiguration
    {
        private readonly ILogger? _logger;
        private readonly TConfig _config;

        public readonly Dictionary<string, string> DefaultHeaders = new();

        private readonly HttpClient _client;

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

        protected void SetAuthorizationData(AlfabankAction action)
        {
            if (string.IsNullOrEmpty(action.Token))
            {
                // Token not provided
                if (string.IsNullOrEmpty(action.Login))
                {
                    action.Login = _config.Login;
                    action.Password = _config.Password;
                    action.Token = _config.Token;
                    // Credentials not provided as well
                    //if (string.IsNullOrEmpty(_config.Token))
                    //{
                    //}
                    //else
                }
            }
        }

        public async Task<string> CallActionRawAsync(AlfabankAction action)
        {
            _logger?.LogTrace("Calling \"{action}\"", action.Action);

            SetAuthorizationData(action);

            Dictionary<string, string> queryParams = action.GetActionQueryParams();
            var content = new FormUrlEncodedContent(queryParams);

            var response = await _client.PostAsync(action.Action, content)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);
        }

        protected static readonly Regex _errorRegex = new Regex("\"errorCode\"\\s*:\\s*\"(\\d+)\"\\s*,\\s*\"errorMessage\"\\s*:\\s*\"(.*?)\"", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary></summary>
        /// <param name="action">Request</param>
        /// <exception cref="AlfabankException"></exception>
        public async Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action)
            where TResponse : class
        {
            var respJson = await CallActionRawAsync(action).ConfigureAwait(false);
            var errorMatch = _errorRegex.Match(respJson);
            if (errorMatch.Success && errorMatch.Groups[1].Value != "0")
                throw new AlfabankException(int.Parse(errorMatch.Groups[1].Value), errorMatch.Groups[2].Value);

            return JsonConvert.DeserializeObject<TResponse>(respJson)!;
        }
    }

    public class AlfabankRestClient : AlfabankRestClient<AlfaBankConfiguration>
    {
        public AlfabankRestClient(AlfaBankConfiguration config) : base(config)
        { }

        public AlfabankRestClient(ILogger<AlfabankRestClient<AlfaBankConfiguration>> logger, AlfaBankConfiguration config)
            : base(logger, config)
        { }
    }
}
