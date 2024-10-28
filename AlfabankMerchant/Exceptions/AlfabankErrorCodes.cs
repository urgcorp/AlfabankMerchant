namespace AlfabankMerchant.Exceptions
{
    public static class AlfabankErrorCodes
    {
        /// <summary>
        /// Обработка запроса прошла без системных ошибок
        /// </summary>
        public static int NO_ERROR => 0;

        /// <summary>
        /// <para>* Заказ с таким номером уже обработан</para>
        /// <para>* Заказ с таким номером был зарегистрирован, но не был оплачен</para>
        /// <para>* Неверный номер заказа</para>
        /// </summary>
        public static int ORDER_ERROR => 1;

        /// <summary>
        /// Неизвестная валюта
        /// </summary>
        public static int UNKNOWN_CURRENCY => 3;

        /// <summary>
        /// <para>* Номер заказа не может быть пуст</para>
        /// <para>* Имя мерчанта не может быть пустым</para>
        /// <para>* Отсутствует сумма</para>
        /// <para>* URL возврата не может быть пуст</para>
        /// <para>* Пароль не может быть пуст</para>
        /// </summary>
        public static int PARAMETER_MISSING => 4;

        /// <summary>
        /// <para>* Логин продавца неверен</para>
        /// <para>* Неверная сумма</para>
        /// <para>* Неправильный параметр 'Язык'</para>
        /// <para>* Доступ запрещён</para>
        /// <para>* Пользователь должен сменить свой пароль</para>
        /// <para>* Доступ запрещён</para>
        /// <para>* jsonParams неверен</para>
        /// </summary>
        public static int BAD_REQUEST => 5;

        /// <summary>
        /// Системная ошибка
        /// </summary>
        public static int SYSTEM_ERROR => 7;

        /// <summary>
        /// <para>* Использование обоих значений Features FORCETDS/FORCESSL и AUTO_PAYMENT недопустимо</para>
        /// <para>* Мерчант не имеет привилегии выполнять AUTO платежи</para>
        /// <para>* Мерчант не имеет привилегии выполнять проверочные платежи</para>
        /// </summary>
        public static int PAYMENT_OPTIONS_ERROR => 13;

        /// <summary>
        /// Features указаны некорректно
        /// </summary>
        public static int BAD_FEATURES => 14;
    }
}
