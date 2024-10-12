namespace alfabank.Abstractions
{
    public interface IAlfabankMerchantService
    {
        /// <summary>
        /// Merchant that client uses
        /// </summary>
        string? Merchant { get; }
    }
}
