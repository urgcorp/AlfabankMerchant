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
        private readonly TConfig _config;

        private readonly HttpClient _client;

        public string? Merchant => _config.Merchant;

        public readonly Dictionary<string, string> DefaultHeaders = new();

        public AlfabankMerchantRestClient(TConfig config)
        {
            _config = config;

            _client = new HttpClient()
            {
                BaseAddress = new Uri(_config.BasePath)
            };
        }

        public AlfabankMerchantRestClient(ILogger<AlfabankMerchantRestClient<TConfig>> logger, TConfig config)
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
                throw new InvalidOperationException($"Unable to determine URL to call for action {action.GetType().Name}.");

            var authConf = action.GetAuthorizationConfig();
            var auth = authentication ?? _config;

            // TODO: Validate authentication
            // TODO: Validate action properties

            Dictionary<string, string> queryParams = action.GetActionParams();
            if (string.IsNullOrEmpty(auth.Login) || string.IsNullOrEmpty(auth.Password))
            {
                if (!string.IsNullOrEmpty(auth.Token))
                    queryParams["token"] = auth.Token;
                else
                    throw new InvalidOperationException($"Action \"{actionUrl}\" require authorization: {authConf.Allowed.ToString(",")}.");
            }
            else
            {
                queryParams["userName"] = auth.Login;
                queryParams["password"] = auth.Password;
            }

            var content = new FormUrlEncodedContent(queryParams);

            _logger?.LogTrace("Calling \"{action}\" for merchant \"{merchant}\".", actionUrl, Merchant);
            var response = await _client.PostAsync(actionUrl, content)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);
        }

        /// <summary></summary>
        /// <param name="action">Request</param>
        /// <exception cref="AlfabankException"></exception>
        public async Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, AuthParams? authentication = null)
            where TResponse : class
        {
            var respJson = await CallActionRawAsync(action, authentication).ConfigureAwait(false);
            
            var resp = JObject.Parse(respJson);
            if (resp.ContainsKey("errorCode") && resp["errorCode"]!.ToString() != "0")
                throw new AlfabankException(resp["errorCode"]!.Value<int>(), resp["errorMessage"]!.Value<string>() ?? "");

            return JsonConvert.DeserializeObject<TResponse>(respJson)!;
        }
    }

    public class AlfabankMerchantRestClient : AlfabankMerchantRestClient<AlfabankConfiguration>
    {
        public AlfabankMerchantRestClient(AlfabankConfiguration config) : base(config)
        { }

        public AlfabankMerchantRestClient(ILogger<AlfabankMerchantRestClient<AlfabankConfiguration>> logger, AlfabankConfiguration config)
            : base(logger, config)
        { }
    }
}
