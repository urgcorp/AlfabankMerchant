using alfabank.ComponentModel;

namespace alfabank
{
    public interface IAlfabankClient
    {
        /// <summary>
        /// Make call to server and return raw response (if HTTP Response is OK)
        /// </summary>
        /// <param name="action">Action request</param>
        /// <returns>Response body</returns>
        /// <exception cref="HttpRequestException"></exception>
        Task<string> CallActionRawAsync(AlfabankAction action);

        /// <summary>
        /// Make call to server and return deserialized response or throw exception if error reported
        /// </summary>
        /// <typeparam name="TResponse">Response type</typeparam>
        /// <param name="action">Action request</param>
        /// <returns>Deserialized response</returns>
        /// <exception cref="AlfabankException"></exception>
        Task<TResponse> CallActionAsync<TResponse>(AlfabankAction<TResponse> action) where TResponse : class;
    }
}
