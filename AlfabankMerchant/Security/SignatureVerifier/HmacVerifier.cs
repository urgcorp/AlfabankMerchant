using System;
using System.Security.Cryptography;
using System.Text;

namespace AlfabankMerchant.Security.SignatureVerifier
{
    /// <summary>
    /// Implementation of symmetric signature verification using HMAC-SHA256.
    /// </summary>
    public class HmacVerifier : ISignatureVerifier
    {
        /// <inheritdoc/>
        public CallbackSignMethod Method => CallbackSignMethod.SYMMETRIC;

        public string CalculateHash(string data, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            using var hmac = new HMACSHA256(keyBytes);
            byte[] hashBytes = hmac.ComputeHash(dataBytes);

            return ToUpperHexString(hashBytes);
        }

        /// <inheritdoc/>
        public bool Verify(string data, string signature, string key)
        {
            var hash = CalculateHash(data, key);
            return string.Equals(hash, signature, StringComparison.OrdinalIgnoreCase);
        }

        private static string ToUpperHexString(byte[] bytes)
        {
            int length = bytes.Length * 2;

            // Выделяем память на стеке (очень быстро, 0 аллокаций в куче)
            // SHA256 дает 32 байта, SHA512 дает 64 байта. Стек легко это выдержит.
            Span<char> span = stackalloc char[length];

            for (int i = 0; i < bytes.Length; i++)
                bytes[i].TryFormat(span.Slice(i * 2), out _, "X2");

            return new string(span);
        }
    }
}