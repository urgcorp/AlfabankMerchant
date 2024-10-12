namespace alfabank
{
    public class AlfabankConfiguration
    {
        public const string REQUEST_SEND_DATETIME_FORMAT = "yyyyMMddHHmmss";

        public string BasePath { get; protected set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public string? Token { get; set; }

        public AlfaBankConfiguration(string basePath = "https://ecom.alfabank.ru/api/")
        {
            BasePath = basePath;
        }
    }
}
