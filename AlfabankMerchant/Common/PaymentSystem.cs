using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.Common
{
    public sealed class PaymentSystem : StringEnum<PaymentSystem>
    {
        public static readonly PaymentSystem VISA = RegisterEnum("VISA");

        public static readonly PaymentSystem MASTERCARD = RegisterEnum("MASTERCARD");

        public static readonly PaymentSystem AMEX = RegisterEnum("AMEX");

        public static readonly PaymentSystem JCB = RegisterEnum("JCB");

        public static readonly PaymentSystem CUP = RegisterEnum("CUP");

        public static readonly PaymentSystem MIR = RegisterEnum("MIR");
    }
}
