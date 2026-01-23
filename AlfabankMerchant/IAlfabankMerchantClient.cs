using System;
using System.Threading;
using System.Threading.Tasks;
using AlfabankMerchant.Common;
using AlfabankMerchant.Exceptions;

namespace AlfabankMerchant
{
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

    public interface IAlfabankMerchantClient<in TConfig> : IAlfabankMerchantClient, IAlfabankMerchant
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