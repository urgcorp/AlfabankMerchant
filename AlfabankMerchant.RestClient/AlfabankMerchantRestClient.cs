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
    public class AlfabankMerchantRestClient : IAlfabankMerchantClient
    {
        protected const string CLIENT_TYPE = "REST";

        protected readonly ILogger? _logger;

        public readonly Dictionary<string, string> DefaultHeaders = new();

        protected HttpClient _client;

        protected AlfabankMerchantRestClient(ILogger? logger)
        {
            _logger = logger;
            _client = null!;
        }

        public AlfabankMerchantRestClient(ILogger<AlfabankMerchantRestClient>? logger = null)
        {
            _logger = logger;
            _client = new();
        }

        public async Task<string> CallActionRawAsync(AlfabankAction action, AlfabankConfiguration configuration, CancellationToken cancellationToken = default)
        {
            if (configuration == null)
                throw ConfigRequiredException();
            if (string.IsNullOrEmpty(configuration.BasePath))
                throw new InvalidOperationException("Client gateway URL not provided. Define client BasePath in configuration.");
            var actionUrl = action.FindDefaultActionUrl(CLIENT_TYPE);
            if (string.IsNullOrEmpty(actionUrl))
                throw new InvalidOperationException($"Unable to determine URL to call for action {action.GetType().Name}.");
            var actionAuth = action.GetAuthorizationConfig();

            // TODO: Validate authentication
            // TODO: Validate action properties

            Dictionary<string, string> queryParams = action.GetActionParams();
            if (configuration.AuthMethod == AuthMethod.TOKEN || 
                (configuration.AuthMethod == AuthMethod.UNDEFINED && string.IsNullOrEmpty(configuration.Login) || string.IsNullOrEmpty(configuration.Password)))
            {
                if (!string.IsNullOrEmpty(configuration.Token))
                    queryParams["token"] = configuration.Token;
            }
            else
            {
                queryParams["userName"] = configuration.Login!;
                queryParams["password"] = configuration.Password!;
            }

            var content = new FormUrlEncodedContent(queryParams);
            if (!string.IsNullOrEmpty(configuration.Merchant))
                _logger?.LogTrace("Calling \"{action}\" for merchant \"{merchant}\".", actionUrl, configuration.Merchant);
            else
                _logger?.LogTrace("Calling \"{action}\".", actionUrl);

            var callUrl = new Uri(new Uri(configuration.BasePath), actionUrl);
            HttpResponseMessage? response;
            response = await _client.PostAsync(callUrl, content, cancellationToken)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary></summary>
        /// <param name="action">Request</param>
        /// <exception cref="AlfabankException"></exception>
        public async Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, AlfabankConfiguration configuration, CancellationToken cancellationToken = default)
            where TResponse : class
        {
            var respJson = await CallActionRawAsync(action, configuration, cancellationToken)
                .ConfigureAwait(false);
            
            var resp = JObject.Parse(respJson);
            if (resp.ContainsKey("errorCode") && resp["errorCode"]!.ToString() != "0")
                throw new AlfabankException(resp["errorCode"]!.Value<int>(), resp["errorMessage"]!.Value<string>() ?? "");

            return JsonConvert.DeserializeObject<TResponse>(respJson)!;
        }

        protected InvalidOperationException ConfigRequiredException() => new InvalidOperationException($"Client was not initialized with configuration. Use method with configuration parameter instead.");
    }

    public class AlfabankMerchantRestClient<TConfig> : AlfabankMerchantRestClient, IAlfabankMerchantClient<TConfig>
        where TConfig : AlfabankConfiguration
    {
        protected readonly TConfig? _config;

        public AlfabankMerchantRestClient(ILogger<AlfabankMerchantRestClient>? logger = null, TConfig? config = null)
        {
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

        public string? Merchant => _config?.Merchant;

        public Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, CancellationToken cancellationToken = default) where TResponse : class
        {
            if (_config == null)
                throw ConfigRequiredException();

            return CallActionAsync(action, _config, cancellationToken);
        }

        public Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, TConfig? configuration = null, CancellationToken cancellationToken = default) 
            where TResponse : class
        {
            return CallActionAsync(action, configuration, cancellationToken);
        }
    }
}
