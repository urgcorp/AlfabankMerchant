namespace AlfabankMerchant.Security
{
    /// <summary>
    /// Callback request checksum implementation
    /// </summary>
    public enum CallbackSignMethod
    {
        /// <summary>
        /// Реализация симметричной криптографии - для формирования контрольной суммы на стороне шлюза
        /// и для её проверки на стороне продавца используется один и тот же (симметричный) криптографический ключ
        /// </summary>
        SYMMETRIC,
        /// <summary>
        /// Реализация асимметричной криптографии - для формирования контрольной суммы на стороне платёжного шлюза
        /// используется закрытый ключ, известный только шлюзу, а для подтверждения контрольной суммы
        /// используется связанный с закрытым ключом открытый ключ, который известен продавцам и может распространяться свободно.
        /// </summary>
        ASYMMETRIC
    }

    /// <summary>
    /// Defines a contract for verifying callback signatures from the bank.
    /// </summary>
    public interface ISignatureVerifier
    {
        /// <summary>
        /// Gets the signature method supported by this verifier.
        /// </summary>
        CallbackSignMethod Method { get; }

        /// <summary>
        /// Verifies the authenticity of the data using the provided signature and key.
        /// </summary>
        /// <param name="data">The concatenated string of parameters to verify.</param>
        /// <param name="signature">The checksum or digital signature received from the bank.</param>
        /// <param name="key">
        /// For <c>HMAC</c>: The raw secret token.
        /// For <c>RSA</c>: The Base64-encoded public key string (without PEM headers and line breaks).
        /// </param>
        /// <returns>True if the signature is valid; otherwise, false.</returns>
        bool Verify(string data, string signature, string key);
    }
}