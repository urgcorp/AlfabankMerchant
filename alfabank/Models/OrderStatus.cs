namespace alfabank.Models
{
    public enum OrderStatus
    {
        /// <summary>
        /// Заказ зарегистрирован, но не оплачен
        /// </summary>
        Registred = 0,
        /// <summary>
        /// Предавторизованная сумма захолдирована (для двухстадийных платежей)
        /// </summary>
        PreAuthorized = 1,
        /// <summary>
        /// Проведена полная авторизация суммы заказа
        /// </summary>
        Authorized = 2,
        /// <summary>
        /// Авторизация отменена
        /// </summary>
        Cancelled = 3,
        /// <summary>
        /// По транзакции была проведена операция возврата
        /// </summary>
        Refund = 4,
        /// <summary>
        /// Инициирована авторизация через ACS банка-эмитента
        /// </summary>
        ACSAuthorization = 5,
        /// <summary>
        /// Авторизация отклонена
        /// </summary>
        Rejected = 6
    }
}
