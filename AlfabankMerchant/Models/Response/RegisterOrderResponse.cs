using System.Diagnostics;
using Newtonsoft.Json;

namespace AlfabankMerchant.Models.Response
{
    [DebuggerDisplay("{OrderId}")]
    public class RegisterOrderResponse
    {
        /// <summary>
        /// <para>Номер заказа в платёжной системе</para>
        /// <para>Уникален в пределах системы</para>
        /// Отсутствует если регистрация заказа на удалась по причине ошибки, детализированной в <see cref="AlfabankResponse.ErrorCode"/>
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// <para>URL платёжной формы, на который надо перенаправить броузер клиента</para>
        /// Не возвращается если регистрация заказа не удалась по причине ошибки, детализированной в <see cref="AlfabankResponse.ErrorCode"/>
        /// </summary>
        [JsonProperty("formUrl")]
        public string FormUrl { get; set; }
    }
}
