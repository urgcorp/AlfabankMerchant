using AlfabankMerchant.Common;

namespace AlfabankMerchant.Services.Components
{
    public interface IAlfabankMerchantBindingService : IAlfabankMerchant
    {
        /// <summary>
        /// Запрос списка связок клиента
        /// </summary>
        Task GetBindingsAsync(object action, AuthParams? authParams = null);

        /// <summary>
        /// Запрос списка связок определённой банковской карты
        /// </summary>
        Task GetBindingsByCardOrIdAsync(object action, AuthParams? authParams = null);

        /// <summary>
        /// Запрос активации связки
        /// </summary>
        Task BindCardAsync(object action, AuthParams? authParams = null);

        /// <summary>
        /// Запрос деактивации связки
        /// </summary>
        Task UnbindCardAsync(object action, AuthParams? authParams = null);

        /// <summary>
        /// Запрос изменения срока действия связки
        /// </summary>
        Task ExtendBindingAsync(object action, AuthParams? authParams = null);
    }
}
