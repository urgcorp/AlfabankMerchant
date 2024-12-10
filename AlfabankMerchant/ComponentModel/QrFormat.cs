namespace AlfabankMerchant.ComponentModel
{
    /// <summary>
    /// Формат QR кода
    /// </summary>
    public class QrFormat : StringEnum<QrFormat>
    {
        public QrFormat(string value) : base(value)
        { }

        /// <summary>
        /// Матрица из нулей и единиц
        /// </summary>
        public static readonly QrFormat Matrix = RegisterEnum(new QrFormat("matrix"));

        /// <summary>
        /// Изображение в Base64
        /// </summary>
        public static readonly QrFormat Image = RegisterEnum(new QrFormat("image"));
    }
}
