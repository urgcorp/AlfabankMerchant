using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.Models
{
    public class QrStatusSbp : StringEnum<QrStatusSbp>
    {
        /// <summary>
        /// QR-код cформирован
        /// </summary>
        public static readonly QrStatusSbp STARTED = RegisterEnum("STARTED");

        /// <summary>
        /// Заказ принят к оплате
        /// </summary>
        public static readonly QrStatusSbp CONFIRMED = RegisterEnum("CONFIRMED");

        /// <summary>
        /// Оплата отклонена
        /// </summary>
        public static readonly QrStatusSbp REJECTED = RegisterEnum("REJECTED");

        /// <summary>
        /// Оплата по QR-коду отклонена мерчантом
        /// </summary>
        public static readonly QrStatusSbp REJECTED_BY_USER = RegisterEnum("REJECTED_BY_USER");

        /// <summary>
        /// Заказ оплачен
        /// </summary>
        public static readonly QrStatusSbp ACCEPTED = RegisterEnum("ACCEPTED");
    }
}
