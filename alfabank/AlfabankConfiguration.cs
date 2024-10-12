namespace alfabank
{
    public class AlfabankConfiguration
    {
        /// <summary>
        /// Base server URL to send requests
        /// </summary>
        public string BasePath { get; protected set; }

        /// <summary>
        /// Идентификатор мерчанта - торгово-сервисного предприятия (ТСП), продающее товары или оказывающее услуги через интернет-сайт
        /// </summary>
        public string? Merchant { get; protected set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        /// <summary>
        /// Merchant payment token
        /// </summary>
        public string? Token { get; set; }

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
