using AlfabankMerchant.Common;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant
{
    public interface IAlfabankMerchantClient : IAlfabankMerchant
    {
        /// <summary>
        /// Make call to server and return deserialized response or throw exception if error reported
        /// </summary>
        /// <typeparam name="TResponse">Response type</typeparam>
        /// <param name="action">Action request</param>
        /// <returns>Deserialized response</returns>
        /// <exception cref="AlfabankException"></exception>
        Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action, AuthParams? authentication = null)
            where TResponse : class;
    }

    public interface IAlfabankMerchantClient<TConfig> : IAlfabankMerchantClient
        where TConfig : AlfabankConfiguration
    { 
    }
}
