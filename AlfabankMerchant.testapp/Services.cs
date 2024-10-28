using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using AlfabankMerchant.Services;

namespace AlfabankMerchant.testapp
{
    public static class Services
    {
        private static ILogger _logger;
        private static ILoggerFactory _loggerFactory;
        private static AlfabankConfiguration _config;
        private static IAlfabankClient _client;

        public static IAlfabankOrderingService OrderService { get; private set; }

        public static void Init<TConfig, TClient>(ILogger logger, ILoggerFactory loggerFactory, TConfig cfg, TClient abClient)
            where TConfig : AlfabankConfiguration
            where TClient : class, IAlfabankClient<TConfig>
        {
            _logger = logger;
            _loggerFactory = loggerFactory;
            _config = cfg;
            _client = abClient;

            var oSvcLogger = _loggerFactory.CreateLogger<AlfabankOrderingService<TConfig, TClient>>();
            OrderService = new AlfabankOrderingService<TConfig, TClient>(oSvcLogger, abClient);
        }
    }
}
