namespace AlfabankMerchant.ComponentModel
{
    /// <summary>
    /// Method of authorization when calling actions
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter.Newtonsoft.StringEnumConverter<AuthMethod>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonConverter.StringEnumConverter<AuthMethod>))]
    public sealed class AuthMethod : StringEnum<AuthMethod>
    {
        public const string UNDEFINED_METHOD = "UNDEFINED";

        public const string PUBLIC_METHOD = "NONE";

        public const string LOGIN_METHOD = "LOGIN";

        public const string TOKEN_METHOD = "TOKEN";

        private AuthMethod(string value) : base(value)
        { }

        public static readonly AuthMethod UNDEFINED = RegisterEnum(new AuthMethod(UNDEFINED_METHOD));

        /// <summary>
        /// Without authorization
        /// </summary>
        public static readonly AuthMethod PUBLIC = RegisterEnum(new AuthMethod(PUBLIC_METHOD));

        /// <summary>
        /// Login and password from alfabank
        /// </summary>
        public static readonly AuthMethod LOGIN = RegisterEnum(new AuthMethod(LOGIN_METHOD));

        /// <summary>
        /// Payment token from alfabank
        /// </summary>
        public static readonly AuthMethod TOKEN = RegisterEnum(new AuthMethod(TOKEN_METHOD));

        /// <summary>
        /// Available authorization methods
        /// </summary>
        public static readonly AuthMethod[] AVAILABLE = new[] { LOGIN, TOKEN };

        public static implicit operator AuthMethod[](AuthMethod obj) => new[] { obj };
    }
}
