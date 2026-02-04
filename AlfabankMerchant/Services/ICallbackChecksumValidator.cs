using AlfabankMerchant.Models;

namespace AlfabankMerchant.Services
{
    /// <summary>
    /// Service for calculating and validating of checksum received with Alfabank Callback requests
    /// </summary>
    public interface ICallbackChecksumValidator
    {
        /// <summary>
        /// Calculates checksum for callback data
        /// </summary>
        /// <returns></returns>
        string CalculateChecksum(AlfabankOperationCallback callback);

        /// <summary>
        /// Validate that given 
        /// </summary>
        /// <param name="callback">Pairs of parameters and their values</param>
        /// <param name="checksum">Given checksum</param>
        /// <returns></returns>
        bool IsValid(AlfabankOperationCallback callback, string checksum);
    }
}