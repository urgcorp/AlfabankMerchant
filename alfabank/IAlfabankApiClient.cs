namespace alfabank
{
    public interface IAlfabankApiClient
    {
        /// <summary>
        /// Регистрация заказа с предавторизацией
        /// </summary>
        Task RegisterPreAuthAsync();

        /// <summary>
        /// Регистрация заказа
        /// </summary>
        Task RegisterAsync();

        /// <summary>
        /// Расширенный запрос состояния заказа
        /// </summary>
        Task GetOrderStatusExtended();

        /// <summary>
        /// Запрос статистики по платежам за период
        /// </summary>
        Task GetLastOrdersForMerchant();
    }
}
