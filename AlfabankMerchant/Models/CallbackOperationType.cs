using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.Models
{
    /// <summary>
    /// Тип операции, о которой пришло уведомление
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter.Newtonsoft.StringEnumConverter<CallbackOperationType>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonConverter.StringEnumConverter<CallbackOperationType>))]
    public class CallbackOperationType : StringEnum<CallbackOperationType>
    {
        private CallbackOperationType(string value) : base(value)
        { }

        /// <summary>
        /// Операция удержания (холдирования) суммы
        /// </summary>
        public static readonly CallbackOperationType Approved = RegisterEnum(new CallbackOperationType("approved"));

        /// <summary>
        /// Операция завершения
        /// </summary>
        public static readonly CallbackOperationType Deposited = RegisterEnum(new CallbackOperationType("deposited"));

        /// <summary>
        /// Операция отмены
        /// </summary>
        public static readonly CallbackOperationType Reversed = RegisterEnum(new CallbackOperationType("reversed"));

        /// <summary>
        /// Операция возврата
        /// </summary>
        public static readonly CallbackOperationType Refunded = RegisterEnum(new CallbackOperationType("refunded"));

        /// <summary>
        /// Истекло время, отпущенное на оплату заказа
        /// </summary>
        public static readonly CallbackOperationType Timeout = RegisterEnum(new CallbackOperationType("declinedByTimeout"));
    }
}
