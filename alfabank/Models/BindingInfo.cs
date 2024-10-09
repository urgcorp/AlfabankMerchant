using System.Diagnostics;
using Newtonsoft.Json;

namespace alfabank.Models
{
    [DebuggerDisplay("{ClientId} - {BindingId}")]
    public class BindingInfo
    {
        /// <summary>
        /// Номер (идентификатор) клиента в системе магазина
        /// </summary>
        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Идентификатор связки, использованной для оплаты
        /// </summary>
        [JsonProperty("bindingId")]
        public string BindingId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("authDateTime")]
        public string? AuthUnixDateTime { get; set; }

        /// <summary>
        /// Reference number
        /// </summary>
        [JsonProperty("authRefNum")]
        public string? AuthRefNum { get; set; }

        /// <summary>
        /// Id терминала
        /// </summary>
        [JsonProperty("terminalId")]
        public string? TerminalId { get; set; }
    }
}
