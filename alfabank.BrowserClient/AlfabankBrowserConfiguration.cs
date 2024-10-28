namespace alfabank.BrowserClient
{
    public class AlfabankBrowserConfiguration : AlfabankConfiguration
    {
        public string? UserAgent { get; set; }

        public AlfabankBrowserConfiguration(string loginPageUrl)
            : base(loginPageUrl)
        { }

        public AlfabankBrowserConfiguration(string loginPageUrl, string merchant)
            : base(loginPageUrl, merchant)
        { }
    }
}
