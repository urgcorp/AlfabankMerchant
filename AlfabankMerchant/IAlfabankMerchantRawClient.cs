using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant
{
    /// <summary>
    /// Client that returns response as raw string with content
    /// </summary>
    public interface IAlfabankMerchantRawClient
    {
        /// <summary>
        /// Make call to server
        /// </summary>
        /// <param name="actionUrl">
        /// <para>Absolute or relative call path</para>
        /// <para>Depends on <see cref="configuration"/> provides BasePath</para>
        /// </param>
        /// <param name="queryParams"></param>
        /// <param name="configuration">Configuration to use with this request</param>
        /// <param name="cancellationToken"></param>
        /// <returns>RAW result in client format</returns>
        Task<string> CallRawAsync(string actionUrl, Dictionary<string, string> queryParams, AlfabankConfiguration? configuration, CancellationToken cancellationToken = default);

        /// <summary>
        /// Make call to server
        /// </summary>
        /// <param name="action">Action request</param>
        /// <param name="configuration">Configuration to use with this request</param>
        /// <param name="cancellationToken"></param>
        /// <returns>RAW result in client format</returns>
        Task<string> CallActionRawAsync(AlfabankAction action, AlfabankConfiguration configuration, CancellationToken cancellationToken = default);
    }

    public interface IAlfabankMerchantRawClient<in TConfig> : IAlfabankMerchantRawClient
        where TConfig : AlfabankConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionUrl"></param>
        /// <param name="queryParams"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>RAW result in client format</returns>
        Task<string> CallRawAsync(string actionUrl, Dictionary<string, string> queryParams, CancellationToken cancellationToken = default);

        Task<string> CallRawAsync(string actionUrl, Dictionary<string, string> queryParams, TConfig? configuration, CancellationToken cancellationToken = default);

        /// <summary>
        /// Make call to server with client configuration
        /// </summary>
        /// <param name="action">Action request</param>
        /// <param name="cancellationToken"></param>
        /// <returns>RAW result in client format</returns>
        Task<string> CallActionRawAsync(AlfabankAction action, CancellationToken cancellationToken = default);

        Task<string> CallActionRawAsync(AlfabankAction action, TConfig? configuration, CancellationToken cancellationToken = default);
    }
}
