namespace AlfabankMerchant.ComponentModel
{
    /// <summary>
    /// Method of authorization when calling actions
    /// </summary>
    public sealed class AuthMethod : StringEnum<AuthMethod>
    {
        public const string UNDEFINED_METHOD = "UNDEFINED";

        public const string PUBLIC_METHOD = "NONE";

        public const string LOGIN_METHOD = "LOGIN";

        public const string TOKEN_METHOD = "TOKEN";

        public static readonly AuthMethod UNDEFINED = RegisterEnum("UNDEFINED");

        /// <summary>
        /// Without authorization
        /// </summary>
        public static readonly AuthMethod PUBLIC = RegisterEnum(PUBLIC_METHOD);

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
