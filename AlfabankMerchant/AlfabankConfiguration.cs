using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant
{
    public class AlfabankConfiguration
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

        /// <summary>
        /// Merchant payment token
        /// </summary>
        public string? Token { get; set; }

        private AuthMethod _authMethod;
        /// <summary>
        /// Authorization method provided by this configuration
        /// </summary>
        public AuthMethod AuthMethod => _authMethod ?? AuthMethod.UNDEFINED;

        public AlfabankConfiguration(string basePath, string merchant, string login, string password)
        {
            BasePath = basePath;
            Merchant = merchant;
            Login = login;
            Password = password;
            _authMethod = AuthMethod.LOGIN;
        }

        public AlfabankConfiguration(string basePath, string merchant, string token)
        {
            BasePath = basePath;
            Merchant = merchant;
            Token = token;
            _authMethod = AuthMethod.TOKEN;
        }

        public AlfabankConfiguration(string basePath)
        {
            BasePath = basePath;
        }

        public AlfabankConfiguration()
        { }

        public void DefineAuthMethod(AuthMethod authMethod) => _authMethod = authMethod;
    }
}
