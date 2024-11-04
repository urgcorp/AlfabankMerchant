namespace AlfabankMerchant
{
    public class AlfabankConfiguration : AuthParams
    {
        /// <summary>
        /// Base server URL to send requests
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// Идентификатор мерчанта - торгово-сервисного предприятия (ТСП), продающее товары или оказывающее услуги через интернет-сайт
        /// </summary>
        public string? Merchant { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public AlfabankConfiguration()
        { }

        public AlfabankConfiguration(string basePath, string merchant)
        {
            BasePath = basePath;
            Merchant = merchant;
        }

        public AlfabankConfiguration(string basePath)
        {
            BasePath = basePath;
        }
    }
}
