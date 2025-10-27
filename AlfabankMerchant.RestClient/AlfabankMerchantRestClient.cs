using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AlfabankMerchant.Exceptions;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.RestClient
{
    /// <summary>
    /// Client is mainly responsible for making the HTTP call to the REST API backend
    /// </summary>
    public class AlfabankMerchantRestClient<TConfig> : IAlfabankMerchantRawClient<TConfig>, IAlfabankMerchantClient<TConfig>
        where TConfig : AlfabankConfiguration
    {
        protected const string CLIENT_TYPE = "REST";

        protected readonly ILogger? _logger;

        protected readonly TConfig? _config;

        protected HttpClient _client = new();

        protected readonly Dictionary<string, string> _defaultHeaders = new();
        
        public string? Merchant => _config?.Merchant;

        public AlfabankMerchantRestClient(ILogger? logger)
        {
            _logger = logger;
        }

        public AlfabankMerchantRestClient(ILogger? logger, TConfig? config = null)
        {
            _logger = logger;
            _config = config;
        }

        public AlfabankMerchantRestClient(ILogger<AlfabankMerchantRestClient<TConfig>>? logger = null)
        {
            _logger = logger;
        }

        public AlfabankMerchantRestClient(ILogger<AlfabankMerchantRestClient<TConfig>>? logger = null, TConfig? config = null)
        {
            _logger = logger;
            _config = config;
        }

        #region IAlfabankMerchantRawClient

        public virtual async Task<string> CallRawAsync(string actionUrl, Dictionary<string, string> queryParams, AlfabankConfiguration? configuration, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(actionUrl))
                throw new AggregateException(nameof(actionUrl));
            
            if (!string.IsNullOrEmpty(configuration?.Merchant))
                _logger?.LogTrace("Calling \"{action}\" for merchant \"{merchant}\".", actionUrl, configuration.Merchant);
            else
                _logger?.LogTrace("Calling \"{action}\".", actionUrl);
            
            Uri callUrl;
            try
            {
                if (!Uri.IsWellFormedUriString(actionUrl, UriKind.Absolute))
                {
                    if (!string.IsNullOrEmpty(configuration?.BasePath))
                        callUrl = new Uri(new Uri(configuration.BasePath), actionUrl);
                    else if (!string.IsNullOrEmpty(_config?.BasePath))
                        callUrl = new Uri(new Uri(_config.BasePath), actionUrl);
                    else
                        throw new InvalidOperationException("When configuration not provided actionUrl must be an absolute URI.");
                }
                else
                    callUrl = new Uri(actionUrl);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Failed to form URI for action from '{_config!.BasePath}' and '{actionUrl}'.", ex);
            }

            var content = new FormUrlEncodedContent(queryParams);
            var response = await _client.PostAsync(callUrl, content, cancellationToken)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                _logger?.LogWarning("{statucCode} {statusReason} - {endpoint}",
                    response.StatusCode, response.ReasonPhrase, callUrl);
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Make call to server
        /// </summary>
        /// <param name="action">Action request</param>
        /// <param name="configuration">Configuration to use with this request</param>
        /// <param name="cancellationToken"></param>
        /// <returns>JSON result in from Alfabank</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public virtual Task<string> CallActionRawAsync(AlfabankAction action, AlfabankConfiguration configuration, CancellationToken cancellationToken = default)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            if (string.IsNullOrEmpty(configuration.BasePath) && string.IsNullOrEmpty(_config?.BasePath))
                throw new ArgumentException("Client gateway URL not provided. Define client BasePath in configuration.", nameof(configuration));
            var actionUrl = action.FindActionUrl(CLIENT_TYPE);
            if (string.IsNullOrEmpty(actionUrl))
                throw new ArgumentException($"Unable to determine URL to call for action {action.GetType().Name}.", nameof(action));

            // TODO: Validate authentication
            // TODO: Validate action properties

            Dictionary<string, string?> queryParams = action.GetActionParams();
            if (configuration.AuthMethod.Equals(AuthMethod.TOKEN) ||
                (configuration.AuthMethod.Equals(AuthMethod.UNDEFINED) && string.IsNullOrEmpty(configuration.Login) || string.IsNullOrEmpty(configuration.Password)))
            {
                if (!string.IsNullOrEmpty(configuration.Token))
                    queryParams["token"] = configuration.Token;
            }
            else
            {
                queryParams["userName"] = configuration.Login ?? "";
                queryParams["password"] = configuration.Password ?? "";
            }
            
            // null parameters shouldn't be used in query
            var qpValNotNull = queryParams.Where(x => x.Value != null);
            queryParams = new Dictionary<string, string?>(qpValNotNull);
            return CallRawAsync(actionUrl, queryParams!, configuration, cancellationToken);
        }

        public Task<string> CallRawAsync(string actionUrl, Dictionary<string, string> queryParams, CancellationToken cancellationToken = default)
        {
            ThrowIfNoConfiguration();
            return CallRawAsync(actionUrl, queryParams, (AlfabankConfiguration)_config!, cancellationToken);
        }

        public Task<string> CallActionRawAsync(AlfabankAction action, CancellationToken cancellationToken = default)
        {
            ThrowIfNoConfiguration();
            return CallActionRawAsync(action, (AlfabankConfiguration)_config!, cancellationToken);
        }

        public Task<string> CallRawAsync(string actionUrl, Dictionary<string, string> queryParams, TConfig? configuration, CancellationToken cancellationToken = default)
        {
            if (configuration == null)
                ThrowIfNoConfiguration();

            return CallRawAsync(actionUrl, queryParams, (AlfabankConfiguration)(configuration ?? _config!), cancellationToken);
        }

        public Task<string> CallActionRawAsync(AlfabankAction action, TConfig? configuration, CancellationToken cancellationToken = default)
        {
            if (configuration == null)
                ThrowIfNoConfiguration();

            return CallActionRawAsync(action, (AlfabankConfiguration)(configuration ?? _config!), cancellationToken);
        }

        #endregion

        public virtual async Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action,
            AlfabankConfiguration configuration,
            CancellationToken cancellationToken = default)
            where TResponse : class
        {
            var respJson = await CallActionRawAsync(action, configuration, cancellationToken)
                .ConfigureAwait(false);

            var resp = JObject.Parse(respJson);
            if (resp.ContainsKey("errorCode") && resp["errorCode"]!.ToString() != "0")
                throw new AlfabankException(resp["errorCode"]!.Value<int>(), resp["errorMessage"]?.Value<string>() ?? "");

            return JsonConvert.DeserializeObject<TResponse>(respJson)!;
        }

        public Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action,
            TConfig? configuration = null,
            CancellationToken cancellationToken = default)
            where TResponse : class
        {
            if (configuration == null)
                ThrowIfNoConfiguration();

            return CallActionAsync(action, (AlfabankConfiguration)(configuration ?? _config!), cancellationToken);
        }

        public Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action,
            CancellationToken cancellationToken = default) where TResponse : class
        {
            ThrowIfNoConfiguration();
            return CallActionAsync(action, (AlfabankConfiguration)_config!, cancellationToken);
        }

        protected void ThrowIfNoConfiguration()
        {
            if (_config == null)
                throw new InvalidOperationException("Client was not initialized with default configuration.");
        }
    }

    /// <summary>
    /// Client is mainly responsible for making the HTTP call to the REST API backend
    /// <para>Uses <see cref="AlfabankConfiguration"/></para>
    /// </summary>
    public class AlfabankMerchantRestClient : AlfabankMerchantRestClient<AlfabankConfiguration>
    {
        public AlfabankMerchantRestClient(ILogger? logger) : base(logger)
        { }

        public AlfabankMerchantRestClient(ILogger? logger, AlfabankConfiguration? config = null) : base(logger, config)
        { }

        public AlfabankMerchantRestClient(ILogger<AlfabankMerchantRestClient>? logger = null) : base(logger)
        { }

        public AlfabankMerchantRestClient(ILogger<AlfabankMerchantRestClient>? logger = null, AlfabankConfiguration? config = null) : base(logger, config)
        { }
    }
}
