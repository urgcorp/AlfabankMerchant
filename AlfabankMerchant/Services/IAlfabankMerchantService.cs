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

    public interface IAlfabankService<TConfig, TClient> : IAlfabankMerchantService
        where TConfig : AlfabankConfiguration
        where TClient : IAlfabankMerchantClient<TConfig>
    { }

    public interface IAlfabankService<TClient> : IAlfabankService<AlfabankConfiguration, TClient>
        where TClient : IAlfabankMerchantClient<AlfabankConfiguration>
    { }
}
