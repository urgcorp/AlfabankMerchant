namespace alfabank.Actions
{
    public static class AlfabankActions
    {
        /// <summary>
        ///  Запросы, используемые при двухстадийной оплате
        /// </summary>
        public static class PreAuth
        {
            /// <summary>
            /// Регистрация заказа с предавторизацией
            /// </summary>
            public const string RegisterOrderPreAuth = "rest/registerPreAuth.do";

            /// <summary>
            /// Запрос завершения оплаты заказа
            /// </summary>
            public const string DepositOrder = "rest/deposit.do";
        }

        /// <summary>
        /// Регистрация заказа
        /// </summary>
        public const string RegisterOrder = "rest/register.do";

        /// <summary>
        /// Запрос отмены оплаты заказа
        /// </summary>
        public const string ReverseOrder = "rest/reverse.do";

        /// <summary>
        /// Запрос возврата средств оплаты заказа
        /// </summary>
        public const string Refund = "rest/refund.do";

        /// <summary>
        /// Запрос сведений о кассовом чеке
        /// </summary>
        public const string ReceiptStatus = "rest/getReceiptStatus.do";

        /// <summary>
        /// Расширенный запрос состояния заказа
        /// </summary>
        public const string GetOrderStatusExtended = "rest/getOrderStatusExtended.do";

        /// <summary>
        /// Запрос статистики по платежам за период
        /// </summary>
        public const string GetLastOrdersForMerchants = "rest/getLastOrdersForMerchants.do";

        /// <summary>
        /// Запрос списка связок определённой банковской карты
        /// </summary>
        public const string GetBindingsByCardOrId = "rest/getBindingsByCardOrId.do";

        /// <summary>
        /// Запрос списка связок клиента
        /// </summary>
        public const string GetBindings = "rest/getBindings.do";

        /// <summary>
        /// Запрос состояния заказа
        /// </summary>
        public const string GetOrderStatus = "rest/getOrderStatus.do";

        /// <summary>
        /// Запрос проверки вовлеченности карты в 3DS
        /// </summary>
        public const string VerifyEnrollment = "rest/verifyEnrollment.do";

        /// <summary>
        /// Запрос проведения платежа по связке
        /// </summary>
        public const string PaymentOrderBinding = "rest/paymentOrderBinding.do";

        /// <summary>
        /// Запрос оплаты через внешнюю платёжную систему
        /// </summary>
        public const string PaymenToTherWay = "rest/paymentotherway.do";

        /// <summary>
        /// Запрос изменения срока действия связки
        /// </summary>
        public const string ExtendBinding = "rest/extendBinding.do";

        /// <summary>
        /// Запрос добавления дополнительных параметров к заказу
        /// </summary>
        public const string AddOrderParams = "rest/addParams.do";

        /// <summary>
        /// Запрос активации связки
        /// </summary>
        public const string BindCard = "rest/bindCard.do";

        /// <summary>
        /// Запрос деактивации связки
        /// </summary>
        public const string UnBindCard = "rest/unBindCard.do";

        /// <summary>
        /// Запрос на регулярный платёж
        /// </summary>
        public const string RecurrentPayment = "recurrentPayment.do";

        public static class SBP
        {
            /// <summary>
            /// Запрос получения QR кода по СБП
            /// </summary>
            public const string GetPaymentQr = "rest/sbp/c2b/qr/dynamic/get.do";

            /// <summary>
            /// Запрос получения статуса платежа по QR коду по СБП
            /// </summary>
            public const string GetPaymentStatus = "rest/sbp/c2b/qr/status.do";
        }

        public static class Apps
        {
            /// <summary>
            /// Запрос на оплату через Alfa Pay
            /// </summary>
            public const string AlfapayPayment = "alfapay/payment.do";

            /// <summary>
            /// Запрос на оплату через Samsung Pay
            /// </summary>
            public const string SamsungPayment = "samsung/payment.do";

            /// <summary>
            /// Запрос на оплату через Apple Pay
            /// </summary>
            public const string ApplePayment = "applepay/payment.do";

            /// <summary>
            /// Запрос на оплату через Google Pay
            /// </summary>
            public const string GooglePayment = "google/payment.do";
        }
    }
}
