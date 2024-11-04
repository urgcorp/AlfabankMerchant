namespace AlfabankMerchant.ComponentModel
{
    /// <summary>
    /// Authorization settings for action
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ActionAuthorizationAttribute : Attribute
    {
        /// <summary>
        /// Allowed authorization methods for action request
        /// </summary>
        public AuthMethod[] Allowed { get; protected set; } = AuthMethod.LOGIN; // By default only login authorization supported

        protected ActionAuthorizationAttribute()
        { }

        /// <summary>
        /// Allow authorization with defined methods
        /// <para>Available methods in <see cref="AuthMethod"/></para>
        /// </summary>
        /// <param name="allowed"></param>
        /// <param name="priority">Preferable authorization method for action</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public ActionAuthorizationAttribute(string[] allowed)
        {
            if (allowed == null)
                throw new ArgumentNullException(nameof(allowed));
            if (!allowed.All(x => AuthMethod.Exists(x)))
                throw new ArgumentOutOfRangeException(nameof(allowed));

            Allowed = allowed.Select(x => AuthMethod.Parse(x)).ToArray();
        }
    }

    /// <summary>
    /// Authorization settings for action that prioritize login and password to authorize
    /// </summary>
    public class LoginAuthorizationAttribute : ActionAuthorizationAttribute
    {
        /// <summary>
        /// Allow authorization preferring login method
        /// </summary>
        /// <param name="allowToken">Allow to authorize using token with this action</param>
        public LoginAuthorizationAttribute(bool allowToken = false) : base()
        {
            if (allowToken)
                Allowed = new[] { AuthMethod.LOGIN, AuthMethod.TOKEN };
        }
    }
}
