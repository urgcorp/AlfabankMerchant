using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.Models
{
    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter.Newtonsoft.StringEnumConverter<QrStatusSbp>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonConverter.StringEnumConverter<QrStatusSbp>))]
    public class QrStatusSbp : StringEnum<QrStatusSbp>
    {
        public QrStatusSbp(string value) : base(value)
        { }

        /// <summary>
        /// QR-код cформирован
        /// </summary>
        public static readonly QrStatusSbp STARTED = RegisterEnum(new QrStatusSbp("STARTED"));

        /// <summary>
        /// Заказ принят к оплате
        /// </summary>
        public static readonly QrStatusSbp CONFIRMED = RegisterEnum(new QrStatusSbp("CONFIRMED"));

        /// <summary>
        /// Оплата отклонена
        /// </summary>
        public static readonly QrStatusSbp REJECTED = RegisterEnum(new QrStatusSbp("REJECTED"));

        /// <summary>
        /// Оплата по QR-коду отклонена мерчантом
        /// </summary>
        public static readonly QrStatusSbp REJECTED_BY_USER = RegisterEnum(new QrStatusSbp("REJECTED_BY_USER"));

        /// <summary>
        /// Заказ оплачен
        /// </summary>
        public static readonly QrStatusSbp ACCEPTED = RegisterEnum(new QrStatusSbp("ACCEPTED"));
    }
}
