namespace AlfabankMerchant.ComponentModel
{
    /// <summary>
    /// Method of authorization when calling actions
    /// </summary>
    public sealed class AuthMethod : StringEnum<AuthMethod>
    {
        public const string LOGIN_METHOD = "LOGIN";

        public const string TOKEN_METHOD = "TOKEN";

        /// <summary>
        /// Login and password from alfabank
        /// </summary>
        public static readonly AuthMethod LOGIN = RegisterEnum(LOGIN_METHOD);

        /// <summary>
        /// Payment token from alfabank
        /// </summary>
        public static readonly AuthMethod TOKEN = RegisterEnum(TOKEN_METHOD);

        /// <summary>
        /// Avaliable authorization methods
        /// </summary>
        public static readonly AuthMethod[] AVAILABLE = new[] { LOGIN, TOKEN };


        public static implicit operator AuthMethod[](AuthMethod obj) => new[] { obj };
    }
}
