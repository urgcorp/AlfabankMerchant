using AlfabankMerchant.ComponentModel;
using AlfabankMerchant.Models;

namespace AlfabankMerchant.Actions
{
    /// <summary>
    /// Запрос получения текущего состояния заказа
    /// </summary>
    [LoginAuthorization]
    [RestUrl("rest/getOrderStatus.do")]
    [WSUrl("soap/merchant-ws")]
    public class GetOrderStatusAction : AlfabankAction<Order>
    {
        /// <summary>
        /// <para>Номер заказа в платёжной системе</para>
        /// <para>Уникален в пределах системы</para>
        /// </summary>
        [ActionProperty("orderId", Type = "ANS36")]
        public string? OrderId { get; set; }

        /// <summary>
        /// <para>Язык в кодировке ISO 639-1. Если не указан, считается, что язык – русский</para>
        /// <para>Сообщение ошибке будет возвращено именно на этом языке</para>
        /// </summary>
        [ActionProperty("language", Type = "A2")]
        public string? Language { get; set; }
    }
}
