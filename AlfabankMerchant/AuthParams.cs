namespace AlfabankMerchant
{
    public class AuthParams
    {
        public string? Login { get; set; }

        public string? Password { get; set; }

        /// <summary>
        /// Merchant payment token
        /// </summary>
        public string? Token { get; set; }

        public static AuthParams FromToken(string token)
            => new AuthParams() { Token = token };

        public static AuthParams FromLogin(string login, string password)
            => new AuthParams() { Login = login, Password = password };
    }
}
