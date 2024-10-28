using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.Models
{
    public sealed class TransactionState : StringEnum<TransactionState>
    {
        public static readonly TransactionState CREATED = RegisterEnum("CREATED");

        public static readonly TransactionState APPROVED = RegisterEnum("APPROVED");

        public static readonly TransactionState DEPOSITED = RegisterEnum("DEPOSITED");

        public static readonly TransactionState DECLINED = RegisterEnum("DECLINED");

        public static readonly TransactionState REVERSED = RegisterEnum("REVERSED");

        public static readonly TransactionState REFUNDED = RegisterEnum("REFUNDED");

        public static readonly TransactionState[] ALL = new[] { CREATED, APPROVED, DEPOSITED, DECLINED, REVERSED, REFUNDED };

        public static implicit operator TransactionState[](TransactionState obj) => new[] { obj };
    }
}
