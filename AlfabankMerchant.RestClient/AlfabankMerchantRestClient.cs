using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AlfabankMerchant.Exceptions;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.RestClient
{
    /// <summary>
    /// Client is mainly responible for making the HTTP call to the REST API backend
    /// </summary>
    public class AlfabankMerchantRestClient<TConfig> : IAlfabankMerchantClient<TConfig>
        where TConfig : AlfabankConfiguration
    {
        protected const string CLIENT_TYPE = "REST";

        private readonly ILogger? _logger;
        private readonly TConfig? _config;

        private readonly HttpClient _client;

        public string? Merchant => _config?.Merchant;

        public readonly Dictionary<string, string> DefaultHeaders = new();

        public AlfabankMerchantRestClient(ILogger<AlfabankMerchantRestClient<TConfig>>? logger = null, TConfig? config = null)
        {
            _logger = logger;
            _config = config;
            
            if (_config != null)
            {
                _client = new HttpClient()
                {
                    BaseAddress = new Uri(_config.BasePath)
                };
            }
            else
                _client = new();
        }

        public async Task<string> CallActionRawAsync(AlfabankAction action, AlfabankConfiguration? configuration, CancellationToken cancellationToken = default)
        {
            var conf = configuration ?? _config ?? throw ConfigRequiredException();
            if (string.IsNullOrEmpty(conf.BasePath))
                throw new InvalidOperationException("Client gateway URL not provided. Define client BasePath in configuration.");
            var actionUrl = action.FindDefaultActionUrl(CLIENT_TYPE);
            if (string.IsNullOrEmpty(actionUrl))
                throw new InvalidOperationException($"Unable to determine URL to call for action {action.GetType().Name}.");
            var actionAuth = action.GetAuthorizationConfig();

            // TODO: Validate authentication
            // TODO: Validate action properties

            Dictionary<string, string> queryParams = action.GetActionParams();
            if (string.IsNullOrEmpty(conf.Login) || string.IsNullOrEmpty(conf.Password))
            {
                if (!string.IsNullOrEmpty(conf.Token))
                    queryParams["token"] = conf.Token;
                else
                    throw new InvalidOperationException($"Action \"{actionUrl}\" require authorization: {actionAuth.Allowed.ToString(",")}.");
            }
            else
            {
                queryParams["userName"] = conf.Login;
                queryParams["password"] = conf.Password;
            }

            var content = new FormUrlEncodedContent(queryParams);
            _logger?.LogTrace("Calling \"{action}\" for merchant \"{merchant}\".", actionUrl, Merchant);
            HttpResponseMessage? response;
            if (_config == null)
            {
                var callUrl = new Uri(new Uri(conf.BasePath), actionUrl);
                response = await _client.PostAsync(callUrl, content, cancellationToken)
                    .ConfigureAwait(false);
            }
            else
            {
                response = await _client.PostAsync(actionUrl, content, cancellationToken)
                    .ConfigureAwait(false);
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary></summary>
        /// <param name="action">Request</param>
        /// <exception cref="AlfabankException"></exception>
        public async Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, AlfabankConfiguration? configuration, CancellationToken cancellationToken = default)
            where TResponse : class
        {
            var respJson = await CallActionRawAsync(action, configuration, cancellationToken)
                .ConfigureAwait(false);
            
            var resp = JObject.Parse(respJson);
            if (resp.ContainsKey("errorCode") && resp["errorCode"]!.ToString() != "0")
                throw new AlfabankException(resp["errorCode"]!.Value<int>(), resp["errorMessage"]!.Value<string>() ?? "");

            return JsonConvert.DeserializeObject<TResponse>(respJson)!;
        }

        public Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, CancellationToken cancellationToken = default) where TResponse : class
        {
            if (_config == null)
                throw ConfigRequiredException();

            return CallActionAsync(action, null, cancellationToken);
        }

        private InvalidOperationException ConfigRequiredException() => new InvalidOperationException($"Client was not initialized with configuration. Use method with configuration parameter instead.");
    }

    public class AlfabankMerchantRestClient : AlfabankMerchantRestClient<AlfabankConfiguration>
    {
        public AlfabankMerchantRestClient(ILogger<AlfabankMerchantRestClient<AlfabankConfiguration>> logger, AlfabankConfiguration config) : base(logger, config)
        { }

        public AlfabankMerchantRestClient(AlfabankConfiguration config) : base(null, config)
        { }
    }
}
