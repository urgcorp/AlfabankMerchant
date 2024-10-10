using static alfabank.AuthParams;

namespace alfabank.ComponentModel
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ActionAuthorizationAttribute : Attribute
    {
        /// <summary>
        /// Allowed authorization methods for action request
        /// </summary>
        public AuthMethod[] Allowed { get; protected set; } = AuthMethod.LOGIN; // By default only login authorization supported

        /// <summary>
        /// Preferred authorization method for action request
        /// </summary>
        public AuthMethod Priority { get; protected set; } = AuthMethod.LOGIN; // Prioritize login and password credentials if multiple

        protected ActionAuthorizationAttribute()
        { }

        public ActionAuthorizationAttribute(string[] allowed, string? priority = null)
        {
            if (allowed == null)
                throw new ArgumentNullException(nameof(allowed));
            if (!allowed.All(x => AuthMethod.Exists(x)))
                throw new ArgumentOutOfRangeException(nameof(allowed));

            if (priority != null)
            {
                if (!AuthMethod.Exists(priority))
                    throw new ArgumentOutOfRangeException(nameof(priority));

                Priority = AuthMethod.Parse(priority);
            }
            Allowed = allowed.Select(x => AuthMethod.Parse(x)).ToArray();
        }
    }

    public class LoginAuthorizationAttribute : ActionAuthorizationAttribute
    {
        /// <summary></summary>
        /// <param name="allowToken">Payment token can be used to authorize this action request</param>
        public LoginAuthorizationAttribute(bool allowToken = false) : base()
        {
            if (allowToken)
                Allowed = new[] { AuthMethod.LOGIN, AuthMethod.TOKEN };
        }
    }

    public class TokenAuthorizationAttribute : ActionAuthorizationAttribute
    {
        /// <summary></summary>
        /// <param name="allowLogin">Login and password can be used to authorize this action request</param>
        public TokenAuthorizationAttribute(bool allowLogin = true) : base()
        {
            Priority = AuthMethod.TOKEN;

            if (allowLogin)
                Allowed = new[] { AuthMethod.LOGIN, AuthMethod.TOKEN};
            else
                Allowed = new[] { AuthMethod.TOKEN };
        }
    }
}
