using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.Models
{
    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter.Newtonsoft.StringEnumConverter<TransactionState>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonConverter.StringEnumConverter<TransactionState>))]
    public sealed class TransactionState : StringEnum<TransactionState>
    {
        public TransactionState(string value) : base(value)
        { }

        public static readonly TransactionState CREATED = RegisterEnum(new TransactionState("CREATED"));

        public static readonly TransactionState APPROVED = RegisterEnum(new TransactionState("APPROVED"));

        public static readonly TransactionState DEPOSITED = RegisterEnum(new TransactionState("DEPOSITED"));

        public static readonly TransactionState DECLINED = RegisterEnum(new TransactionState("DECLINED"));

        public static readonly TransactionState REVERSED = RegisterEnum(new TransactionState("REVERSED"));

        public static readonly TransactionState REFUNDED = RegisterEnum(new TransactionState("REFUNDED"));

        public static readonly TransactionState[] ALL = new[] { CREATED, APPROVED, DEPOSITED, DECLINED, REVERSED, REFUNDED };

        public static implicit operator TransactionState[](TransactionState obj) => new[] { obj };
    }
}
