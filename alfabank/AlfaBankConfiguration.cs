namespace alfabank
{
    public class AlfaBankConfiguration
    {
        public const string REQUEST_SEND_DATETIME_FORMAT = "yyyyMMddHHmmss";

        public string BasePath { get; protected set; }

        /// <summary>
        /// Идентификатор мерчанта - торгово-сервисного предприятия (ТСП), продающее товары или оказывающее услуги через интернет-сайт
        /// </summary>
        public string Merchant { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public string? Token { get; set; }

        public AlfaBankConfiguration(string basePath = "https://ecom.alfabank.ru/api/")
        {
            BasePath = basePath;
        }
    }
}
