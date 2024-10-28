using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant
{
    /// <summary>
    /// Способ совершения платежа
    /// </summary>
    public sealed class PaymentWay : StringEnum<PaymentWay>
    {
        /// <summary>
        /// Оплата с вводом карточных данных
        /// </summary>
        public static readonly PaymentWay CARD = RegisterEnum("CARD");

        /// <summary>
        /// Оплата связкой
        /// </summary>
        public static readonly PaymentWay CARD_BINDING = RegisterEnum("CARD_BINDING");

        /// <summary>
        /// Оплата с помощью "Альфа-Клик" (через систему PayByClik)
        /// </summary>
        public static readonly PaymentWay ALFA_ALFACLICK = RegisterEnum("ALFA_ALFACLICK");

        /// <summary>
        /// Оплата платежной системой AlfaPay
        /// </summary>
        public static readonly PaymentWay ALFAPAY = RegisterEnum("ALFAPAY");
        
        public static readonly PaymentWay FILE_BINDING = RegisterEnum("FILE_BINDING");
        
        public static readonly PaymentWay FILE_SBP_C2B_BINDING = RegisterEnum("FILE_SBP_C2B_BINDING");

        public static readonly PaymentWay P2P = RegisterEnum("P2P");
        
        public static readonly PaymentWay P2P_BINDING = RegisterEnum("P2P_BINDING");

        public static readonly PaymentWay[] P2P_PAYMENTS = new[] { P2P, P2P_BINDING }; 
        
        public static readonly PaymentWay PAYPAL = RegisterEnum("PAYPAL");
        
        public static readonly PaymentWay APPLE_PAY = RegisterEnum("APPLE_PAY");
        
        public static readonly PaymentWay APPLE_PAY_BINDING = RegisterEnum("APPLE_PAY_BINDING");
        
        public static readonly PaymentWay GOOGLE_PAY_CARD = RegisterEnum("GOOGLE_PAY_CARD");
        
        public static readonly PaymentWay GOOGLE_PAY_RAW = RegisterEnum("GOOGLE_PAY_RAW");
        
        public static readonly PaymentWay GOOGLE_PAY_CARD_BINDING = RegisterEnum("GOOGLE_PAY_CARD_BINDING");
        
        public static readonly PaymentWay GOOGLE_PAY_TOKENIZED = RegisterEnum("GOOGLE_PAY_TOKENIZED");
        
        public static readonly PaymentWay GOOGLE_PAY_TOKENIZED_BINDING = RegisterEnum("GOOGLE_PAY_TOKENIZED_BINDING");

        public static readonly PaymentWay[] GOOGLE_PAY_PAYMENTS = new[] { GOOGLE_PAY_CARD, GOOGLE_PAY_RAW, GOOGLE_PAY_CARD_BINDING, GOOGLE_PAY_TOKENIZED, GOOGLE_PAY_TOKENIZED_BINDING };
        
        public static readonly PaymentWay SAMSUNG_PAY = RegisterEnum("SAMSUNG_PAY");
        
        public static readonly PaymentWay SAMSUNG_PAY_BINDING = RegisterEnum("SAMSUNG_PAY_BINDING");
        
        public static readonly PaymentWay SAMSUNG_PAY_RAW = RegisterEnum("SAMSUNG_PAY_RAW");
        
        public static readonly PaymentWay SENDY = RegisterEnum("SENDY");
        
        public static readonly PaymentWay SBP_C2B = RegisterEnum("SBP_C2B");
        
        public static readonly PaymentWay SBP_C2B_BINDING = RegisterEnum("SBP_C2B_BINDING");

        /// <summary>
        /// Выплаты СБП
        /// </summary>
        public static readonly PaymentWay SBP_B2C = RegisterEnum("SBP_B2C");

        /// <summary>
        /// Оплата СБП (Сервис Быстрых Платежей) для Alfa
        /// </summary>
        public static readonly PaymentWay ALFA_SBP = RegisterEnum("ALFA_SBP");
        
        public static readonly PaymentWay TOKEN_PAY = RegisterEnum("TOKEN_PAY");
        
        public static readonly PaymentWay TOKEN_PAY_BINDING = RegisterEnum("TOKEN_PAY_BINDING");
        
        public static readonly PaymentWay YANDEX_PAY_CARD = RegisterEnum("YANDEX_PAY_CARD");
        
        public static readonly PaymentWay YANDEX_PAY_TOKENIZED = RegisterEnum("YANDEX_PAY_TOKENIZED");
        
        public static readonly PaymentWay YANDEX_PAY_CARD_BINDING = RegisterEnum("YANDEX_PAY_CARD_BINDING");
        
        public static readonly PaymentWay YANDEX_PAY_TOKENIZED_BINDING = RegisterEnum("YANDEX_PAY_TOKENIZED_BINDING");
        
        public static readonly PaymentWay MIR_PAY = RegisterEnum("MIR_PAY");
        
        public static readonly PaymentWay MIR_PAY_BINDING = RegisterEnum("MIR_PAY_BINDING");

        /// <summary>
        /// Оплата через сервис Альфа-банка "Подели"
        /// <para>BNPL (Buy Now Pay Later)</para>
        /// </summary>
        public static readonly PaymentWay PODELI = RegisterEnum("PODELI");
    }
}
