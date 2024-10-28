using AlfabankMerchant.ComponentModel;
using AlfabankMerchant.Models;

namespace AlfabankMerchant.Actions
{
    /// <summary>
    /// Запроса состояния зарегистрированного заказа
    /// </summary>
    [LoginAuthorization]
    [RestUrl("rest/getOrderStatusExtended.do")]
    public sealed class GetOrderStatusExtendedAction : AlfabankAction<Order>
    {
        /// <summary>
        /// <para>Номер заказа в платёжной системе</para>
        /// <para>Уникален в пределах системы</para>
        /// </summary>
        [ActionProperty("orderId", Type = "ANS36")]
        public string? OrderId { get; set; }

        /// <summary>
        /// Номер (идентификатор) заказа в системе магазина
        /// </summary>
        [ActionProperty("orderNumber", Type = "AN..32")]
        public string? OrderNumber { get; set; }

        /// <summary>
        /// Номер (идентификатор) заказа в системе магазина
        /// </summary>
        [ActionProperty("merchantOrderNumber", Type = "ANS..32")]
        public string? MerchantOrderNumber { get; set; }

        /// <summary>
        /// <para>Язык в кодировке ISO 639-1. Если не указан, считается, что язык – русский</para>
        /// <para>Сообщение ошибке будет возвращено именно на этом языке</para>
        /// </summary>
        [ActionProperty("language", Type = "A2")]
        public string? Language { get; set; }
    }
}
