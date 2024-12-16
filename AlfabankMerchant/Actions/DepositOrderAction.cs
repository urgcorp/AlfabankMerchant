using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.Actions
{
    /// <summary>
    /// Запрос списания суммы предавторизации
    /// <para>Данную операцию можно осуществлять, если есть соответствующие права в системе</para>
    /// </summary>
    [RestUrl(AlfabankRestActions.DepositOrder)]
    public class DepositOrderAction : AlfabankAction
    {
    }
}
