using System;
using System.Collections.Generic;
using System.Linq;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.Actions
{
    /// <summary>
    /// Запрос отмены оплаты заказа
    /// <para>Функция отмены доступна в течение ограниченного времени после оплаты, точные сроки необходимо уточнять в Банке</para>
    /// <para>Операция отмены оплаты может быть совершена только один раз. Если она закончится ошибкой, то повторная операция отмены платежа не пройдет</para>
    /// <para>Данная функция доступна магазинам по согласованию с Банком. Для выполнения операции отмены пользователь должен обладать соответствующими правами</para>
    /// </summary>
    [LoginAuthorization(true)]
    [RestUrl("rest/reverse.do")]
    public class ReverseOrderAction : AlfabankAction
    {
        /// <summary>
        /// <para>Номер заказа в платёжной системе</para>
        /// <para>Уникален в пределах системы</para>
        /// </summary>
        [ActionProperty("orderId", true, Type = "ANS36")]
        public string? OrderId { get; set; }

        /// <summary>
        /// <para>Язык в кодировке ISO 639-1. Если не указан, считается, что язык – русский</para>
        /// <para>Сообщение ошибке будет возвращено именно на этом языке</para>
        /// </summary>
        [ActionProperty("language", Type = "A2")]
        public string? Language { get; set; }
    }
}
