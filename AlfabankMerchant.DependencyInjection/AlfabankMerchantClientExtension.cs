using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
        public static void AddAlfabankClient<TClient, TConfig>(this IServiceCollection services, TClient client, TConfig config)
            where TClient : class, IAlfabankMerchantClient<TConfig>
            where TConfig : AlfabankConfiguration
        {
            services.AddSingleton(config);
            services.AddSingleton(client);
            services.AddSingleton<IAlfabankMerchantClient>(s => s.GetRequiredService<TClient>());
        }

        /// <summary>
        /// Register alfabank client and configuration as singletone
        /// </summary>
        /// <param name="config">Client configuration</param>
        public static void AddAlfabankClient<TClient, TConfig>(this IServiceCollection services, TConfig config)
            where TClient : class, IAlfabankMerchantClient<TConfig>
            where TConfig : AlfabankConfiguration
        {
            services.AddSingleton(config);
            services.AddSingleton<IAlfabankMerchantClient, TClient>();
        }

        public static void AddAlfabankClient<TClient, TConfig>(this IServiceCollection services, Action<TConfig> configureOptions)
            where TClient : class, IAlfabankMerchantClient<TConfig>
            where TConfig : AlfabankConfiguration, new()
        {
            var cfg = new TConfig();
            configureOptions(cfg);
            services.AddSingleton(cfg);
            services.AddSingleton<IAlfabankMerchantClient, TClient>();
        }

        public static void AddAlfabankClient<TClient, TConfig>(this IServiceCollection services, IConfiguration configuration)
            where TClient : class, IAlfabankMerchantClient<TConfig>
            where TConfig : AlfabankConfiguration, new()
        {
            services.Configure<TConfig>(configuration);
            services.AddSingleton(s => s.GetRequiredService<IOptions<TConfig>>().Value);
            services.AddSingleton<IAlfabankMerchantClient, TClient>();
        }
    }
}
