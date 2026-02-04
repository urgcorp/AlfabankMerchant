using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AlfabankMerchant.Security.SignatureVerifier
{
    /// <summary>
    /// Implementation of asymmetric digital signature verification using RSA and SHA-512.
    /// </summary>
    public class RsaVerifier : ISignatureVerifier, IDisposable
    {
        /// <inheritdoc/>
        public CallbackSignMethod Method => CallbackSignMethod.ASYMMETRIC;

        public readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;
        public readonly RSASignaturePadding SignaturePadding = RSASignaturePadding.Pkcs1;

        private readonly ConcurrentDictionary<string, RSA> _rsaCache = new ConcurrentDictionary<string, RSA>();

        public RsaVerifier()
        { }

        public RsaVerifier(HashAlgorithmName hashAlgorithm, RSASignaturePadding signaturePadding)
        {
            HashAlgorithm = hashAlgorithm;
            SignaturePadding = signaturePadding;
        }

        /// <inheritdoc />
        /// <remarks>
        /// Expects Base64-encoded public key string (without PEM headers and line breaks).
        /// </remarks>
        public bool Verify(string data, string signatureHex, string base64PublicKey)
        {
            try
            {
                var rsa = _rsaCache.GetOrAdd(base64PublicKey, key =>
                {
                    var instance = RSA.Create();
                    byte[] publicKeyBytes = Convert.FromBase64String(key);
                    instance.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);
                    return instance;
                });

                byte[] signatureBytes = StringToByteArray(signatureHex);
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);

                return rsa.VerifyData(dataBytes, signatureBytes, HashAlgorithm, SignaturePadding);
            }
            catch
            {
                return false;
            }
        }

        private static byte[] StringToByteArray(string hex)
        {
            int length = hex.Length;
            byte[] bytes = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);

            return bytes;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            foreach (var rsa in _rsaCache.Values)
                rsa.Dispose();
            _rsaCache.Clear();
        }
    }
}