using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;

namespace AlfabankMerchant.Models
{
    /// <summary>
    /// Big chungus object that represent callbacks from Alfabank
    /// </summary>
    [DebuggerDisplay("{Operation} {(Status ? 1 : 0)} | {OrderNumber} | {OrderId}")]
    public class AlfabankOperationCallback
    {
        /// <summary>
        /// <para>Аутентификационный код, или контрольная сумма, полученная из набора параметров</para>
        /// <remarks>Не является частью проверяемых параметров, так что не включен в список всех параметров</remarks>
        /// </summary>
        [DataMember(Name = "checksum")]
        public string? Checksum { get; set; }

        /// <summary>
        /// <para>Алиас алгоритма, что использовался для получения <see cref="Checksum"/></para>
        /// <remarks>Не является частью проверяемых параметров, так что не включен в список всех параметров</remarks>
        /// </summary>
        [DataMember(Name = "sign_alias")]
        public string? SignAlias { get; set; }

        public Dictionary<string, string?> AllParameters { get; } =
            new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Уникальный номер заказа в платёжной системе.
        /// </summary>
        [DataMember(Name = "mdOrder")]
        public string OrderId
        {
            get => Get("mdOrder")!;
            set => AllParameters["mdOrder"] = value;
        }

        /// <summary>
        /// Уникальный номер (идентификатор) заказа в системе магазина.
        /// </summary>
        [DataMember(Name = "orderNumber")]
        public string OrderNumber
        {
            get => Get("orderNumber")!;
            set => AllParameters["orderNumber"] = value;
        }

        /// <summary>
        /// Время создания запроса уведомления обратного вызова
        /// </summary>
        [DataMember(Name = "callbackCreationDate")]
        public string? CreationDate
        {
            get => Get("callbackCreationDate");
            set => AllParameters["callbackCreationDate"] = value;
        }

        /// <summary>
        /// Тип операции, о которой пришло уведомление
        /// </summary>
        [DataMember(Name = "operation")]
        public CallbackOperationType? Operation
        {
            get
            {
                var value = Get("operation");
                if (value == null || !CallbackOperationType.TryParse(value, out var result))
                    return null;
                return result;
            }
            set => AllParameters["operation"] = value?.ToString();
        }

        /// <summary>
        /// Индикатор успешности операции, указанной в параметре <see cref="Operation"/>
        /// </summary>
        [DataMember(Name = "status")]
        public bool? Status
        {
            get => ParseBool(Get("status"));
            set => AllParameters["status"] = FormatBool(value, true);
        }

        [DataMember(Name = "amount")]
        public int? Amount
        {
            get => int.TryParse(Get("amount"), out var value) ? value : (int?)null;
            set => AllParameters["amount"] = value?.ToString();
        }

        /// <summary>
        /// <para>Идентификатор связки</para>
        /// </summary>
        [DataMember(Name = "bindingId")]
        public string? BindingId
        {
            get => Get("bindingId");
            set => AllParameters["bindingId"] = value;
        }

        /// <summary>
        /// <para>Идентификатор покупателя в системе магазина</para>
        /// <example>Логин, Id</example>
        /// </summary>
        [DataMember(Name = "clientId")]
        public string? ClientId
        {
            get => Get("clientId");
            set => AllParameters["clientId"] = value;
        }

        /// <summary>
        /// <para>Указывает на то, активирована ли связка.</para>
        /// <para><c>true</c> - активирована.<br/><c>false</c> - деактивирована.</para>
        /// </summary>
        [DataMember(Name = "enabled")]
        public bool? Enabled
        {
            get => ParseBool(Get("enabled"));
            set => AllParameters["enabled"] = FormatBool(value, false);
        }

        /// <summary>
        /// Параметр возвращает сумму частичного возврата в минимальных значениях
        /// </summary>
        [DataMember(Name = "operationRefundedAmount")]
        public int? RefundedAmount
        {
            get => int.TryParse(Get("operationRefundedAmount"), out var value) ? value : (int?)null;
            set => AllParameters["operationRefundedAmount"] = value?.ToString();
        }

        /// <summary>
        /// Параметр возвращает сумму частичного возврата в минимальных значениях
        /// и отформатированную в соответствии с валютой
        /// </summary>
        [DataMember(Name = "operationRefundedAmountFormatted")]
        public string? RefundedAmountFormatted
        {
            get => Get("operationRefundedAmountFormatted");
            set => AllParameters["operationRefundedAmountFormatted"] = value;
        }

        /// <summary>
        /// Format a parameters string used to calculate checksum
        /// </summary>
        public string ToParametersString()
        {
            int count = AllParameters.Count;
            if (count == 0) return
                string.Empty;

            string[] keys = ArrayPool<string>.Shared.Rent(count);
            try
            {
                int actualCount = 0;
                foreach (var key in AllParameters.Keys)
                    keys[actualCount++] = key;

                // Sort with O(N log N)
                Array.Sort(keys, 0, actualCount, StringComparer.OrdinalIgnoreCase);

                var sb = new StringBuilder(actualCount * 16); 

                for (int i = 0; i < actualCount; i++)
                {
                    var key = keys[i];
                
                    // Faster to compare directly than against exclude list
                    if (string.Equals(key, "checksum", StringComparison.OrdinalIgnoreCase) || string.Equals(key, "sign_alias", StringComparison.OrdinalIgnoreCase))
                        continue;

                    var val = AllParameters[key];
                    if (string.IsNullOrEmpty(val))
                        sb.Append(key).Append(";;");

                    sb.Append(key).Append(';').Append(val).Append(';');
                }

                return sb.ToString();
            }
            finally
            {
                ArrayPool<string>.Shared.Return(keys);
            }
        }

        #region Helpers

        /// <summary>
        /// Read value from <see cref="AllParameters"/> collection
        /// </summary>
        /// <returns>Value or empty string if key exists<br/><c>null</c> if key does not exist</returns>
        private string? Get(string key) => AllParameters.TryGetValue(key, out var val) ? val ?? string.Empty : null;

        /// <summary>
        /// Parse string value to boolean (0\1, true\false)
        /// </summary>
        private static bool? ParseBool(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            if (value == "1" || value.Equals("true", StringComparison.OrdinalIgnoreCase))
                return true;
            if (value == "0" || value.Equals("false", StringComparison.OrdinalIgnoreCase))
                return false;
            return null;
        }

        private static string? FormatBool(bool? value, bool useNumeric)
        {
            if (value == null)
                return null;
            if (useNumeric)
                return value.Value ? "1" : "0";
            return value.Value ? "true" : "false";
        }

        #endregion
    }
}
