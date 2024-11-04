using AlfabankMerchant.ComponentModel;
using AlfabankMerchant.Models;
using AlfabankMerchant.Models.Response;

namespace AlfabankMerchant.Actions
{
    /// <summary>
    /// Запрос получения статистики по платежам за определённый период
    /// </summary>
    [LoginAuthorization]
    [RestUrl(AlfabankRestActions.GetLastOrdersForMerchants)]
    public sealed class GetLastOrdersForMerchantsAction : AlfabankAction<LastOrdersForMerchants>
    {
        /// <summary>
        /// Язык в кодировке ISO 639-1. Если не указан, считается, что язык – русский.
        /// <para></para>Сообщение ошибке будет возвращено именно на этом языке.
        /// </summary>
        [ActionProperty("language", Type = "A2")]
        public string? Language { get; set; }

        /// <summary>
        /// При обработке запроса будет сформирован список, разбитый на страницы (с количеством записей size на одной странице).
        /// <para></para>В ответе возвращается страница под номером, указанным в параметре page. Нумерация страниц начинается с 0.
        /// <para></para>Если параметр не указан, будет возвращена страница под номером 0.
        /// </summary>
        [ActionProperty("page", Type = "N")]
        public int? Page { get; set; } = 0;

        /// <summary>
        /// Количество элементов на странице (максимальное значение = 200).
        /// </summary>
        [ActionProperty("size", true, Type = "N..3")]
        public int? Size { get; set; } = 25;

        /// <summary>
        /// Дата и время начала периода для выборки заказов в формате YYYYMMDDHHmmss.
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// <see cref="FromDate"/>
        /// </summary>
        [ActionProperty("from", true, Type = "ANS")]
        private string? FromDateParam => GetDTParamValue(FromDate);

        /// <summary>
        /// Дата и время окончания периода для выборки заказов в формате YYYYMMDDHHmmss.
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// <see cref="ToDate"/>
        /// </summary>
        [ActionProperty("to", true, Type = "ANS")]
        private string? ToDateParam => GetDTParamValue(ToDate);

        /// <summary>
        /// transactionStates
        /// </summary>
        public TransactionState[]? TransactionStates { get; set; }

        /// <summary>
        /// <see cref="TransactionStates"/>
        /// </summary>
        [ActionProperty("transactionStates", true, Type = "A..9")]
        private string? TransactionStateParam => TransactionStates != null ? TransactionState.ToString(TransactionStates) : null;

        /// <summary>
        /// Список Логинов мерчантов, чьи транзакции должны попасть в отчёт.
        /// <para></para>Оставьте это значение пустым, чтобы получить список отчётов по всем доступным мерчантам (дочерним мерчантам и мерчантам, указанным в настройках пользователя).
        /// </summary>
        public string[]? Merchants { get; set; } = Array.Empty<string>();

        /// <summary>
        /// 
        /// </summary>
        [ActionProperty("merchants", true, Type = "ANS", AllowEmpty = true)]
        private string? MerchantsParam => Merchants != null ? string.Join(",", Merchants) : null;

        /// <summary>
        /// true – поиск заказов, дата создания которых попадает в заданный период
        /// <para></para>false – поиск заказов, дата оплаты которых попадает в заданный период (таким образом, в отчёте не могут присутствовать заказы в статусе CREATED и DECLINED).
        /// <para></para>Значение по умолчанию – false
        /// </summary>
        public bool? SearchByCreatedDate { get; set; }

        /// <summary>
        /// <see cref="SearchByCreatedDate"/>
        /// </summary>
        [ActionProperty("searchByCreatedDate", Type = "A..5")]
        private string? SearchByCreatedDateParam => GetBoolParamVal(SearchByCreatedDate);
    }
}
