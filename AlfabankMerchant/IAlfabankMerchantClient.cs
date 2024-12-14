using AlfabankMerchant.Common;
using AlfabankMerchant.ComponentModel;
using AlfabankMerchant.Exceptions;

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
        /// <param name="action">Action request</param>
        /// <param name="configuration">Configuration to use with this request</param>
        /// <returns>RAW result in client format</returns>
        Task<string> CallActionRawAsync(AlfabankAction action, AlfabankConfiguration configuration, CancellationToken cancellationToken = default);
    }

    public interface IAlfabankMerchantClient
    {
        /// <summary>
        /// Make call to server
        /// </summary>
        /// <param name="action">Action request</param>
        /// <param name="configuration">Configuration to use with this request</param>
        /// <returns>Deserialized response</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="AlfabankException"></exception>
        Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, AlfabankConfiguration configuration, CancellationToken cancellationToken = default)
            where TResponse : class;
    }

    public interface IAlfabankMerchantClient<TConfig> : IAlfabankMerchantClient, IAlfabankMerchant
        where TConfig : AlfabankConfiguration
    {
        /// <summary>
        /// Make call to server
        /// </summary>
        /// <param name="action">Action request</param>
        /// <param name="configuration">Configuration to use with this request</param>
        /// <returns>Deserialized response</returns>
        /// <exception cref="AlfabankException"></exception>
        Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, TConfig? configuration, CancellationToken cancellationToken = default)
            where TResponse : class;

        /// <summary>
        /// Make call to server
        /// </summary>
        /// <param name="action">Action request</param>
        /// <returns>Deserialized response</returns>
        /// <exception cref="AlfabankException"></exception>
        Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, CancellationToken cancellationToken = default)
            where TResponse : class;
    }
}
