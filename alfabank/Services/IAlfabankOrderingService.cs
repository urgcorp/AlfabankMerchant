using alfabank.Abstractions;
using alfabank.Actions;
using alfabank.Models.Response;

namespace alfabank.Services
{
    /// <summary>
    /// Service that provides ability to register new orders
    /// <para>Only method that can be called without permissin (Requires merchant token)</para>
    /// </summary>
    public interface IAlfabankOrderingService : IAlfabankMerchantService
    {
        /// <summary>
        /// Register new order
        /// </summary>
        Task<RegisterOrderResponse> RegisterOrderAsync(RegisterOrderAction action, AuthParams? auth = null);

        /// <summary>
        /// Register new order
        /// </summary>
        /// <param name="orderNumber">Order number within the shop</param>
        /// <param name="currency">Payment currency</param>
        /// <param name="amount">Payment amount in cents or kopecks</param>
        /// <param name="returnUrl">Address where client will be redirected after successfull payment (like https://example.com/payment_success)</param>
        /// <param name="description">Free form order description</param>
        /// <param name="email">Payer email</param>
        /// <param name="phone">Payer phone number</param>
        /// <param name="clientId">Client ID within shop system</param>
        /// <param name="clientIp">Client IP address</param>
        /// <param name="auth">Override authentication params</param>
        Task<RegisterOrderResponse> RegisterOrderAsync(string orderNumber, Currency currency, int amount, string returnUrl, string? description,
            string? email = null, string? phone = null, string? clientId = null, string? clientIp = null,
            AuthParams? auth = null);
    }
}
