﻿using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.Models
{
    /// <summary>
    /// Формат QR кода
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter.Newtonsoft.StringEnumConverter<QrFormat>))]
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonConverter.StringEnumConverter<QrFormat>))]
    public class QrFormat : StringEnum<QrFormat>
    {   
        private QrFormat(string value) : base(value)
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
