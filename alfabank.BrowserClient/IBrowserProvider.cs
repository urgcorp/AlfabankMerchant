using PuppeteerSharp;

namespace alfabank.BrowserClient
{
    public interface IBrowserProvider
    {
        Task<IBrowser> GetBrowserAsync();
    }
}
