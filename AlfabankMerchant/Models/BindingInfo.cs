using System.Diagnostics;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AlfabankMerchant.Models
{
    [DebuggerDisplay("{ClientId} - {BindingId}")]
    public class BindingInfo
    {
        /// <summary>
        /// Номер (идентификатор) клиента в системе магазина
        /// </summary>
        [JsonProperty("clientId")]
        [JsonPropertyName("clientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Идентификатор связки, использованной для оплаты
        /// </summary>
        [JsonProperty("bindingId")]
        [JsonPropertyName("bindingId")]
        public string BindingId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("authDateTime")]
        [JsonPropertyName("authDateTime")]
        public string? AuthUnixDateTime { get; set; }

        /// <summary>
        /// Reference number
        /// </summary>
        [JsonProperty("authRefNum")]
        [JsonPropertyName("authRefNum")]
        public string? AuthRefNum { get; set; }

        /// <summary>
        /// Id терминала
        /// </summary>
        [JsonProperty("terminalId")]
        [JsonPropertyName("terminalId")]
        public string? TerminalId { get; set; }
    }
}
