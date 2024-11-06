using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.DependencyInjection
{
    public static class AlfabankMerchantClientExtension
    {
        /// <summary>
        /// Register provided alfabank client and configuration as singleton
        /// <para>Also register <see cref="IAlfabankMerchantClient"/> from provided client</para>
        /// </summary>
        /// <param name="client">Alfabank client</param>
        /// <param name="config">Client configuration</param>
        public static void AddAlfabankMerchantClient<TClient, TConfig>(this IServiceCollection services, TClient client, TConfig config)
            where TClient : class, IAlfabankMerchantClient<TConfig>
            where TConfig : AlfabankConfiguration
        {
            services.AddSingleton(config);
            services.AddSingleton(client);
            services.AddSingleton<IAlfabankMerchantClient<TConfig>>(s => s.GetRequiredService<TClient>());
            services.AddSingleton<IAlfabankMerchantClient>(s => s.GetRequiredService<TClient>());
        }

        /// <summary>
        /// Register alfabank client and configuration as singletone
        /// </summary>
        /// <param name="config">Client configuration</param>
        public static void AddAlfabankMerchantClient<TClient, TConfig>(this IServiceCollection services, TConfig config)
            where TClient : class, IAlfabankMerchantClient<TConfig>
            where TConfig : AlfabankConfiguration
        {
            services.AddSingleton(config);
            services.AddSingleton<TClient>();
            services.AddSingleton<IAlfabankMerchantClient<TConfig>, TClient>(s => s.GetRequiredService<TClient>());
            services.AddSingleton<IAlfabankMerchantClient>(s => s.GetRequiredService<TClient>());
        }

        /// <summary>
        /// Register alfabank client and configuration as singletone
        /// </summary>
        /// <param name="configureOptions">Client configuration definition</param>
        /// <param name="authMethod">Authorization method that expected to be provided by this configuration</param>
        public static void AddAlfabankMerchantClient<TClient, TConfig>(this IServiceCollection services, Action<TConfig> configureOptions, AuthMethod? authMethod = null)
            where TClient : class, IAlfabankMerchantClient<TConfig>
            where TConfig : AlfabankConfiguration, new()
        {
            var cfg = new TConfig();
            if (authMethod != null)
                cfg.DefineAuthMethod(authMethod);
            else
                cfg.DefineAuthMethod(AuthMethod.UNDEFINED);

            configureOptions(cfg);
            services.AddSingleton(cfg);
            services.AddSingleton<TClient>();
            services.AddSingleton<IAlfabankMerchantClient<TConfig>>(s => s.GetRequiredService<TClient>());
            services.AddSingleton<IAlfabankMerchantClient>(s => s.GetRequiredService<TClient>());
        }

        /// <summary>
        /// Register alfabank client and configuration as singletone
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
                else
                    cfg.DefineAuthMethod(AuthMethod.UNDEFINED);
            });
            services.AddSingleton(s => s.GetRequiredService<IOptions<TConfig>>().Value);
            services.AddSingleton<TClient>();
            services.AddSingleton<IAlfabankMerchantClient<TConfig>, TClient>(s => s.GetRequiredService<TClient>());
            services.AddSingleton<IAlfabankMerchantClient>(s => s.GetRequiredService<TClient>());
        }

        /// <summary>
        /// Register alfabank client for certain configuration type as sigleton without own configuration
        /// </summary>
        public static void AddAlfabankMerchantClient<TClient, TConfig>(this IServiceCollection services)
            where TClient : class, IAlfabankMerchantClient<TConfig>
            where TConfig : AlfabankConfiguration
        {
            services.AddSingleton<TClient>();
            services.AddSingleton<IAlfabankMerchantClient<TConfig>, TClient>(s => s.GetRequiredService<TClient>());
            services.AddSingleton<IAlfabankMerchantClient>(s => s.GetRequiredService<TClient>());
        }

        /// <summary>
        /// Registre alfabank client with no configuration as singletone
        /// </summary>
        public static void AddAlfabankMerchantClient<TClient>(this IServiceCollection services)
            where TClient : class, IAlfabankMerchantClient
        {
            services.AddSingleton<TClient>();
            services.AddSingleton<IAlfabankMerchantClient>(s => s.GetRequiredService<TClient>());
        }
    }
}
