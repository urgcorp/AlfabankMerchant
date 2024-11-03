using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.Models
{
    /// <summary>
    /// Тип операции, о которой пришло уведомление
    /// </summary>
    public class CallbackOperationType : StringEnum<CallbackOperationType>
    {
        /// <summary>
        /// Операция удержания (холдирования) суммы
        /// </summary>
        public static readonly CallbackOperationType Approved = RegisterEnum("approved");

        /// <summary>
        /// Операция завершения
        /// </summary>
        public static readonly CallbackOperationType Deposited = RegisterEnum("deposited");

        /// <summary>
        /// Операция отмены
        /// </summary>
        public static readonly CallbackOperationType Reversed = RegisterEnum("reversed");

        /// <summary>
        /// Операция возврата
        /// </summary>
        public static readonly CallbackOperationType Refunded = RegisterEnum("refunded");

        /// <summary>
        /// Истекло время, отпущенное на оплату заказа
        /// </summary>
        public static readonly CallbackOperationType Timeout = RegisterEnum("declinedByTimeout");
    }
}
