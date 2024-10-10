using alfabank.ComponentModel;

namespace alfabank
{
    public class AuthParams
    {
        /// <summary>
        /// Method of authorization when calling actions
        /// </summary>
        public sealed class AuthMethod : StringEnum<AuthMethod>
        {
            /// <summary>
            /// Login and password from alfabank
            /// </summary>
            public static readonly AuthMethod LOGIN = RegisterEnum("LOGIN");

            /// <summary>
            /// Payment token from alfabank
            /// </summary>
            public static readonly AuthMethod TOKEN = RegisterEnum("TOKEN");

            /// <summary>
            /// Avaliable authorization methods
            /// </summary>
            public static readonly AuthMethod[] AVAILABLE = new[] { LOGIN, TOKEN };


            public static implicit operator AuthMethod[](AuthMethod obj) => new[] { obj };
        }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public string? Token { get; set; }
    }
}
