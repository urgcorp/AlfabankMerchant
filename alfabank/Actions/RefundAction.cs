using alfabank.ComponentModel;
using alfabank.Models.Response;

namespace alfabank.Actions
{
    /// <summary>
    /// <para>По этому запросу средства по указанному заказу будут возвращены плательщику</para>
    /// <para>Запрос закончится ошибкой в случае, если средства по этому заказу не были списаны</para>
    /// <para>Система позволяет возвращать средстваболее одного раза, но в общей сложности не более первоначальной суммы списания</para>
    /// <para>При выполнении возврата за оплаты жилищно-коммунальных услуг возможен только полный возврат</para>
    /// <para>Для выполнения операции возврата необходимоналичие соответствующих права в системе</para>
    /// </summary>
    [LoginAuthorization]
    public sealed class RefundAction : AlfabankAction<RefundResponse>
    {
        public override string Action { get; set; } = AlfabankRestActions.Refund;

        /// <summary>
        /// <para>Номер заказа в платёжной системе</para>
        /// <para>Уникален в пределах системы</para>
        /// </summary>
        [ActionProperty("orderId", true, Type = "ANS36")]
        public string OrderId { get; set; }

        [ActionProperty("amount", true, Type = "Сумма платежа в копейках (или центах)")]
        public int Amount { get; set; }

        /// <summary>
        /// Валюта платежа
        /// </summary>
        public Currency? Currency { get; set; }

        /// <summary>
        /// Код валюты платежа ISO 4217
        /// </summary>
        [ActionProperty("currency", Type = "N3")]
        private string? CurrencyCode => Currency?.CurrencyCode.ToString();

        /// <summary>
        /// <para>Дополнительные параметры мерчанта</para>
        /// Данные поля могут быть переданы в процессинг банка для последующего отображения в реестрах. Включение данной функциональности возможно по согласованию с банком в период интеграции
        /// </summary>
        public Dictionary<string, string>? MerchantParams { get; set; }

        /// <summary>
        /// JSON string <see cref="MerchantParams"/>
        /// </summary>
        [ActionProperty("jsonParams", Type = "AN..1024")]
        private string? MerchantParamsJson => GetJsonParamsString(MerchantParams);

        #region MerchantParams
        public string? SubscriptionPurpose
        {
            get => TryGetParamValue(MerchantParams, "subscriptionPurpose");
            set => MerchantParams = TrySetParamValue(MerchantParams, "subscriptionPurpose", value);
        }

        /// <summary>
        /// Включение данного функционала возможно по согласованию с банком в период интеграции
        /// </summary>
        public string? SbpTermNo
        {
            get => TryGetParamValue(MerchantParams, "sbpTermNo");
            set => MerchantParams = TrySetParamValue(MerchantParams, "sbpTermNo", value);
        }
        #endregion

        /// <summary>
        /// <para>Параметр служит в качестве определения, что запрос повторный</para>
        /// Если параметр передан, то его значение сравнивается с текущим значением depositedAmount в заказе.
        /// Операция будет проведена, только если значения совпадают.
        /// <para></para>Если придут два возврата с одинаковым expectedDepositedAmount, то будет проведен только один возврат.
        /// Этот возврат изменит значение depositedAmount, и тогда второй возврат будет отклонен
        /// </summary>
        [ActionProperty("expectedDepositedAmount", Type = "N..12")]
        public int? ExpectedDepositedAmount { get; set; }

        /// <summary>
        /// <para>Идентификатор возврата</para>
        /// При попытке повторного возврата проверяется externalRefundId: если возврат с таким идентификатором уже был, то возвращается успешный ответ с данными по предыдущему возврату, если нет, то выполняется новый возврат.
        /// </summary>
        [ActionProperty("externalRefundId", Type = "AN..30")]
        public string? ExternalRefuntId { get; set; }
    }
}
