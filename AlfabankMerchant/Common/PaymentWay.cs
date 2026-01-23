using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.Common
{
    /// <summary>
    /// Способ совершения платежа
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter.Newtonsoft.StringEnumConverter<PaymentWay>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonConverter.StringEnumConverter<PaymentWay>))]
    public class PaymentWay : StringEnum<PaymentWay>
    {
        private PaymentWay(string value) : base(value)
        { }

        /// <summary>
        /// Оплата с вводом карточных данных
        /// </summary>
        public static readonly PaymentWay CARD = RegisterPW("CARD");

        /// <summary>
        /// Оплата связкой
        /// </summary>
        public static readonly PaymentWay CARD_BINDING = RegisterPW("CARD_BINDING");

        /// <summary>
        /// Оплата с помощью "Альфа-Клик" (через систему PayByClick)
        /// </summary>
        public static readonly PaymentWay ALFA_ALFACLICK = RegisterPW("ALFA_ALFACLICK");

        /// <summary>
        /// Оплата платежной системой AlfaPay
        /// </summary>
        public static readonly PaymentWay ALFAPAY = RegisterPW("ALFAPAY");

        /// <summary>
        /// Оплата через Систему Быстрых Платежей
        /// </summary>
        public static readonly PaymentWay SBP = RegisterPW("SBP");

        public static readonly PaymentWay FILE_BINDING = RegisterPW("FILE_BINDING");

        public static readonly PaymentWay FILE_SBP_C2B_BINDING = RegisterPW("FILE_SBP_C2B_BINDING");

        public static readonly PaymentWay P2P = RegisterPW("P2P");

        public static readonly PaymentWay P2P_BINDING = RegisterPW("P2P_BINDING");

        public static readonly PaymentWay[] P2P_PAYMENTS = new[] { P2P, P2P_BINDING };

        public static readonly PaymentWay PAYPAL = RegisterPW("PAYPAL");

        public static readonly PaymentWay APPLE_PAY = RegisterPW("APPLE_PAY");

        public static readonly PaymentWay APPLE_PAY_BINDING = RegisterPW("APPLE_PAY_BINDING");

        public static readonly PaymentWay GOOGLE_PAY_CARD = RegisterPW("GOOGLE_PAY_CARD");

        public static readonly PaymentWay GOOGLE_PAY_RAW = RegisterPW("GOOGLE_PAY_RAW");

        public static readonly PaymentWay GOOGLE_PAY_CARD_BINDING = RegisterPW("GOOGLE_PAY_CARD_BINDING");

        public static readonly PaymentWay GOOGLE_PAY_TOKENIZED = RegisterPW("GOOGLE_PAY_TOKENIZED");

        public static readonly PaymentWay GOOGLE_PAY_TOKENIZED_BINDING = RegisterPW("GOOGLE_PAY_TOKENIZED_BINDING");

        public static readonly PaymentWay[] GOOGLE_PAY_PAYMENTS = new[] { GOOGLE_PAY_CARD, GOOGLE_PAY_RAW, GOOGLE_PAY_CARD_BINDING, GOOGLE_PAY_TOKENIZED, GOOGLE_PAY_TOKENIZED_BINDING };

        public static readonly PaymentWay SAMSUNG_PAY = RegisterPW("SAMSUNG_PAY");

        public static readonly PaymentWay SAMSUNG_PAY_BINDING = RegisterPW("SAMSUNG_PAY_BINDING");

        public static readonly PaymentWay SAMSUNG_PAY_RAW = RegisterPW("SAMSUNG_PAY_RAW");

        public static readonly PaymentWay SENDY = RegisterPW("SENDY");

        public static readonly PaymentWay SBP_C2B = RegisterPW("SBP_C2B");

        public static readonly PaymentWay SBP_C2B_BINDING = RegisterPW("SBP_C2B_BINDING");

        /// <summary>
        /// Выплаты СБП
        /// </summary>
        public static readonly PaymentWay SBP_B2C = RegisterPW("SBP_B2C");

        /// <summary>
        /// Оплата СБП (Сервис Быстрых Платежей) для Alfa
        /// </summary>
        public static readonly PaymentWay ALFA_SBP = RegisterPW("ALFA_SBP");

        public static readonly PaymentWay TOKEN_PAY = RegisterPW("TOKEN_PAY");

        public static readonly PaymentWay TOKEN_PAY_BINDING = RegisterPW("TOKEN_PAY_BINDING");

        public static readonly PaymentWay YANDEX_PAY_CARD = RegisterPW("YANDEX_PAY_CARD");

        public static readonly PaymentWay YANDEX_PAY_TOKENIZED = RegisterPW("YANDEX_PAY_TOKENIZED");

        public static readonly PaymentWay YANDEX_PAY_CARD_BINDING = RegisterPW("YANDEX_PAY_CARD_BINDING");

        public static readonly PaymentWay YANDEX_PAY_TOKENIZED_BINDING = RegisterPW("YANDEX_PAY_TOKENIZED_BINDING");

        public static readonly PaymentWay MIR_PAY = RegisterPW("MIR_PAY");

        public static readonly PaymentWay MIR_PAY_BINDING = RegisterPW("MIR_PAY_BINDING");

        /// <summary>
        /// Оплата через сервис Альфа-банка "Подели"
        /// <para>BNPL (Buy Now Pay Later)</para>
        /// </summary>
        public static readonly PaymentWay PODELI = RegisterPW("PODELI");

        private static PaymentWay RegisterPW(string value) => RegisterEnum(new PaymentWay(value));
    }
}