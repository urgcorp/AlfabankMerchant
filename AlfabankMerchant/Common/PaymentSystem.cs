using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.Common
{
    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter.Newtonsoft.StringEnumConverter<PaymentSystem>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonConverter.StringEnumConverter<PaymentSystem>))]
    public class PaymentSystem : StringEnum<PaymentSystem>
    {
        private PaymentSystem(string value) : base(value)
        { }

        public static readonly PaymentSystem VISA = RegisterEnum(new PaymentSystem("VISA"));

        public static readonly PaymentSystem MASTERCARD = RegisterEnum(new PaymentSystem("MASTERCARD"));

        public static readonly PaymentSystem MIR = RegisterEnum(new PaymentSystem("MIR"));

        /// <summary>
        /// American Express
        /// </summary>
        public static readonly PaymentSystem AMEX = RegisterEnum(new PaymentSystem("AMEX"));

        /// <summary>
        /// Japan Credit Bureau
        /// </summary>
        public static readonly PaymentSystem JCB = RegisterEnum(new PaymentSystem("JCB"));

        /// <summary>
        /// China Union Pay
        /// </summary>
        public static readonly PaymentSystem CUP = RegisterEnum(new PaymentSystem("CUP"));
    }
}
