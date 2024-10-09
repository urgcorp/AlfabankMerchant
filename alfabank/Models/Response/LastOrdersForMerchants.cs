using Newtonsoft.Json;

namespace alfabank.Models.Response
{
    public sealed class LastOrdersForMerchants
    {
        /// <summary>
        /// Информацию о заказах, попавших в отчёт
        /// </summary>
        [JsonProperty("orderStatuses")]
        public Order[] Items { get; set; }

        /// <summary>
        /// Общее количество элементов в отчёте (на всех страницах).
        /// </summary>
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        /// <summary>
        /// Номер текущей страницы (равный номеру страницы, переданному в запросе).
        /// </summary>
        [JsonProperty("page")]
        public int Page { get; set; }

        /// <summary>
        /// Максимальное количество записей на странице (равно размеру страницы, переданному в запросе).
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
    }
}
