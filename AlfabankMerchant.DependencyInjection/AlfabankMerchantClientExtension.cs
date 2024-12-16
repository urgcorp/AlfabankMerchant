using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.DependencyInjection
{
    public static class AlfabankMerchantClientExtension
    {
        /// <summary>
        /// Register alfabank client with no configuration as singleton
        /// </summary>
        public static void AddAlfabankMerchantClient<TClient>(this IServiceCollection services)
            where TClient : class, IAlfabankMerchantClient
        {
            services.TryAddSingleton<TClient>();
        }

        public static void AddAlfabankRawMerchantClient<TClient>(this IServiceCollection services)
            where TClient : class, IAlfabankMerchantRawClient
        {
            services.TryAddSingleton<IAlfabankMerchantRawClient, TClient>();
        }

        /// <summary>
        /// Register alfabank client for certain configuration type as singleton without own configuration
        /// </summary>
        public static void AddAlfabankMerchantClient<TClient, TConfig>(this IServiceCollection services)
            where TConfig : AlfabankConfiguration
            where TClient : class, IAlfabankMerchantClient<TConfig>
        {
            services.TryAddSingleton<TClient>();
        }

        public static void AddAlfabankRawMerchantClient<TClient, TConfig>(this IServiceCollection services)
            where TConfig : AlfabankConfiguration
            where TClient : class, IAlfabankMerchantClient<TConfig>, IAlfabankMerchantRawClient<TConfig>
        {
            services.TryAddSingleton<TClient>();
        }

        /// <summary>
        /// Register provided alfabank client and configuration as singleton
        /// <para>Also register <see cref="IAlfabankMerchantClient"/> from provided client</para>
        /// </summary>
        /// <param name="client">Alfabank client</param>
        /// <param name="config">Client configuration</param>
        public static void AddAlfabankMerchantClient<TClient, TConfig>(this IServiceCollection services, TClient client, TConfig config)
            where TConfig : AlfabankConfiguration
            where TClient : class, IAlfabankMerchantClient<TConfig>
        {
            services.AddSingleton(config);
            services.AddSingleton(client);
        }

        /// <summary>
        /// Register alfabank client and configuration as singletone
        /// </summary>
        /// <param name="config">Client configuration</param>
        public static void AddAlfabankMerchantClient<TClient, TConfig>(this IServiceCollection services, TConfig config)
            where TConfig : AlfabankConfiguration
            where TClient : class, IAlfabankMerchantClient<TConfig>
        {
            services.AddSingleton(config);
            services.AddSingleton<TClient>();
        }

        /// <summary>
        /// Register alfabank client and configuration as singletone
        /// </summary>
        /// <param name="configureOptions">Client configuration definition</param>
        /// <param name="authMethod">Authorization method that expected to be provided by this configuration</param>
        public static void AddAlfabankMerchantClient<TClient, TConfig>(this IServiceCollection services, Action<TConfig> configureOptions, AuthMethod? authMethod = null)
            where TConfig : AlfabankConfiguration, new()
            where TClient : class, IAlfabankMerchantClient<TConfig>
        {
            var cfg = new TConfig();
            if (authMethod != null)
                cfg.DefineAuthMethod(authMethod);

            configureOptions(cfg);
            services.AddSingleton(cfg);
            services.AddSingleton<TClient>();
        }

        /// <summary>
        /// Register alfabank client and configuration as singleton
        /// </summary>
        /// <param name="configuration">Configuration section with configuration data</param>
        /// <param name="authMethod">Authorization method that expected to be provided by this configuration</param>
        public static void AddAlfabankMerchantClient<TClient, TConfig>(this IServiceCollection services, IConfiguration configuration, AuthMethod? authMethod = null)
            where TClient : class, IAlfabankMerchantClient<TConfig>
            where TConfig : AlfabankConfiguration, new()
        {
            services.Configure<TConfig>(configuration);
            services.PostConfigure<TConfig>(cfg =>
            {
                if (authMethod != null)
                    cfg.DefineAuthMethod(authMethod);
            });
            services.AddSingleton(s => s.GetRequiredService<IOptions<TConfig>>().Value);
            services.AddSingleton<TClient>();
        }
    }
}
