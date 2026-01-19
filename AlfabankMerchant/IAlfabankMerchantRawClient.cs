namespace AlfabankMerchant;

/// <summary>
/// Client that returns response as raw string with content
/// </summary>
public interface IAlfabankMerchantRawClient
{
    /// <summary>
    /// Make call to server
    /// </summary>
    /// <param name="actionUrl">
    /// <para>Absolute or relative call path</para>
    /// <para>Depends on <see cref="configuration"/> provided BasePath</para>
    /// </param>
    /// <param name="queryParams"></param>
    /// <param name="configuration">Configuration to use with this request</param>
    /// <param name="cancellationToken"></param>
    /// <returns>RAW result in client format</returns>
    Task<string> CallRawAsync(string actionUrl, Dictionary<string, string> queryParams, AlfabankConfiguration? configuration, CancellationToken cancellationToken = default);

    /// <summary>
    /// Make call to server
    /// </summary>
    /// <param name="action">Action request</param>
    /// <param name="configuration">Configuration to use with this request</param>
    /// <param name="cancellationToken"></param>
    /// <returns>RAW result in client format</returns>
    Task<string> CallActionRawAsync(AlfabankAction action, AlfabankConfiguration configuration, CancellationToken cancellationToken = default);
}

/// <summary>
/// <para>Client that returns response as raw string with content</para>
/// <para>Uses specific configuration type</para>
/// </summary>
/// <typeparam name="TConfig"></typeparam>
public interface IAlfabankMerchantRawClient<in TConfig> : IAlfabankMerchantRawClient
    where TConfig : AlfabankConfiguration
{
    /// <summary>
    /// Make call to server using client configuration
    /// </summary>
    /// <param name="actionUrl">
    /// <para>Absolute or relative call path</para>
    /// <para>Depends on client configuration provided BasePath</para>
    /// </param>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>RAW result in client format</returns>
    Task<string> CallRawAsync(string actionUrl, Dictionary<string, string> queryParams, CancellationToken cancellationToken = default);

    /// <summary>
    /// Make call to server using provided configuration
    /// </summary>
    /// <param name="actionUrl">
    /// <para>Absolute or relative call path</para>
    /// <para>Depends on <see cref="configuration"/> provides BasePath</para>
    /// </param>
    /// <param name="queryParams"></param>
    /// <param name="configuration">Alfabank configuration</param>
    /// <param name="cancellationToken"></param>
    /// <returns>RAW result in client format</returns>
    Task<string> CallRawAsync(string actionUrl, Dictionary<string, string> queryParams, TConfig? configuration, CancellationToken cancellationToken = default);

    /// <summary>
    /// Make call to server using client configuration
    /// </summary>
    /// <param name="action">Action request</param>
    /// <param name="cancellationToken"></param>
    /// <returns>RAW result in client format</returns>
    Task<string> CallActionRawAsync(AlfabankAction action, CancellationToken cancellationToken = default);

    /// <summary>
    /// Make call to server using provided configuration
    /// </summary>
    /// <param name="action">Action request</param>
    /// <param name="configuration">Alfabank configuration</param>
    /// <param name="cancellationToken"></param>
    /// <returns>RAW result in client format</returns>
    Task<string> CallActionRawAsync(AlfabankAction action, TConfig? configuration, CancellationToken cancellationToken = default);
}