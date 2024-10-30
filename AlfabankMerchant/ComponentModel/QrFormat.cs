namespace AlfabankMerchant.ComponentModel
{
    /// <summary>
    /// Формат QR кода
    /// </summary>
    public class QrFormat : StringEnum<QrFormat>
    {
        /// <summary>
        /// Матрица из нулей и единиц
        /// </summary>
        public static readonly QrFormat Matrix = RegisterEnum("matrix");

        /// <summary>
        /// Изображение в Base64
        /// </summary>
        public static readonly QrFormat Image = RegisterEnum("image");
    }
}
