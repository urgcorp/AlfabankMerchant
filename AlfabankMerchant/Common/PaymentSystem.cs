﻿using AlfabankMerchant.ComponentModel;
using Newtonsoft.Json.Linq;

namespace AlfabankMerchant.Common
{
    public sealed class PaymentSystem : StringEnum<PaymentSystem>
    {
        public PaymentSystem(string value) : base(value)
        { }

        public static readonly PaymentSystem VISA = RegisterEnum(new PaymentSystem("VISA"));

        public static readonly PaymentSystem MASTERCARD = RegisterEnum(new PaymentSystem("MASTERCARD"));

        public static readonly PaymentSystem AMEX = RegisterEnum(new PaymentSystem("AMEX"));

        public static readonly PaymentSystem JCB = RegisterEnum(new PaymentSystem("JCB"));

        public static readonly PaymentSystem CUP = RegisterEnum(new PaymentSystem("CUP"));

        public static readonly PaymentSystem MIR = RegisterEnum(new PaymentSystem("MIR"));
    }
}
