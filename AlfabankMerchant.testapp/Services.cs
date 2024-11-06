using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace AlfabankMerchant.testapp
{
    public static class Services
    {
        private static ILogger _logger;
        private static ILoggerFactory _loggerFactory;
        private static AlfabankConfiguration _config;
        private static IAlfabankMerchantClient _client;

        // public static IAlfabankMerchantOrderingService OrderService { get; private set; }

        public static void Init<TConfig, TClient>(ILogger logger, ILoggerFactory loggerFactory, TConfig cfg, TClient abClient)
            where TConfig : AlfabankConfiguration
            where TClient : class, IAlfabankMerchantClient
        {
            _logger = logger;
            _loggerFactory = loggerFactory;
            _config = cfg;
            _client = abClient;
        }
    }
}
