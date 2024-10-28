namespace AlfabankMerchant.ComponentModel
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ActionUrlAttribute : Attribute
    {
        public readonly string ClientType;

        public readonly string Url;

        public ActionUrlAttribute(string clientType, string url)
        {
            ClientType = clientType;
            Url = url;
        }
    }

    public sealed class RestUrlAttribute : ActionUrlAttribute
    {
        public RestUrlAttribute(string url) : base("REST", url)
        { }
    }

    public sealed class WSUrlAttribute : ActionUrlAttribute
    {
        public WSUrlAttribute(string url) : base("WS", url)
        { }
    }
}
