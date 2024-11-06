using AlfabankMerchant.Common;
using AlfabankMerchant.ComponentModel;
using AlfabankMerchant.Exceptions;

namespace AlfabankMerchant
{
    public interface IAlfabankMerchantClient : IAlfabankMerchant
    {
        /// <summary>
        /// Make call to server
        /// </summary>
        /// <param name="action">Action request</param>
        /// <param name="configuration">Configuration to use with this request</param>
        /// <returns>Deserialized response</returns>
        /// <exception cref="AlfabankException"></exception>
        Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, AlfabankConfiguration? configuration = null, CancellationToken cancellationToken = default)
            where TResponse : class;
    }

    public interface IAlfabankMerchantClient<TConfig> : IAlfabankMerchantClient
        where TConfig : AlfabankConfiguration
    {
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
