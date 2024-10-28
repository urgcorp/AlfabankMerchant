using PuppeteerSharp;

namespace AlfabankMerchant.BrowserClient
{
    public interface IBrowserProvider
    {
        Task<IBrowser> GetBrowserAsync();
    }
}
