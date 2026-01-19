using AlfabankMerchant.Common;

namespace AlfabankMerchant.Services.Components;

public interface IAlfabankMerchantBindingService : IAlfabankMerchant
{
    /// <summary>
    /// Запрос списка связок клиента
    /// </summary>
    Task GetBindingsAsync(object action, AlfabankConfiguration? configuration = null);

    /// <summary>
    /// Запрос списка связок определённой банковской карты
    /// </summary>
    Task GetBindingsByCardOrIdAsync(object action, AlfabankConfiguration? configuration = null);

    /// <summary>
    /// Запрос активации связки
    /// </summary>
    Task BindCardAsync(object action, AlfabankConfiguration? configuration = null);

    /// <summary>
    /// Запрос деактивации связки
    /// </summary>
    Task UnbindCardAsync(object action, AlfabankConfiguration? configuration = null);

    /// <summary>
    /// Запрос изменения срока действия связки
    /// </summary>
    Task ExtendBindingAsync(object action, AlfabankConfiguration? configuration = null);
}