using AlfabankMerchant.Common;
using AlfabankMerchant.Services.Components;

namespace AlfabankMerchant.Services
{
    public interface IAlfabankMerchantService : IAlfabankMerchant,
        IAlfabankMerchantOrderingService,
        IAlfabankMerchantBindingService,
        IAlfabankMerchantPaymentService,
        IAlfabankMerchantSBPService
    { }

    public interface IAlfabankMerchantService<TConfig, TClient> : IAlfabankMerchantService
        where TConfig : AlfabankConfiguration
        where TClient : IAlfabankMerchantClient<TConfig>
    { }

    public interface IAlfabankMerchantService<TClient> : IAlfabankMerchantService<AlfabankConfiguration, TClient>
        where TClient : IAlfabankMerchantClient<AlfabankConfiguration>
    { }
}