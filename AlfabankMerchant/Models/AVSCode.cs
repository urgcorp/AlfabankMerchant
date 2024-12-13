using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.Models
{
    /// <summary>
    /// Код проверки Address Verification Service
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter.Newtonsoft.StringEnumConverter<AVSCode>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonConverter.StringEnumConverter<AVSCode>))]
    public class AVSCode : StringEnum<AVSCode>
    {
        public AVSCode(string value) : base(value)
        { }

        /// <summary>
        /// Почтовый индекс и адрес совпадают
        /// </summary>
        public static readonly AVSCode A = RegisterEnum(new AVSCode("A"));

        /// <summary>
        /// Адрес совпадает, почтовый индекс не совпадает
        /// </summary>
        public static readonly AVSCode B = RegisterEnum(new AVSCode("B"));

        /// <summary>
        /// Почтовый индекс совпадает, адрес не совпадает
        /// </summary>
        public static readonly AVSCode C = RegisterEnum(new AVSCode("C"));

        /// <summary>
        /// Почтовый индекс и адрес не совпадают
        /// </summary>
        public static readonly AVSCode D = RegisterEnum(new AVSCode("D"));

        /// <summary>
        /// Проверка данных запрошена, но результат неуспешен
        /// </summary>
        public static readonly AVSCode E = RegisterEnum(new AVSCode("E"));

        /// <summary>
        /// Некорректный формат запроса AVS/AVV проверки
        /// </summary>
        public static readonly AVSCode F = RegisterEnum(new AVSCode("F"));
    }
}
