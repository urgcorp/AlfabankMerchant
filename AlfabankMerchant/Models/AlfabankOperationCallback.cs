using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using static AlfabankMerchant.Models.AlfabankOperationCallback;

namespace AlfabankMerchant.Models
{
    public class AlfabankOperationCallback
    {
        public readonly struct OperationCallbackParameter
        {
            public string Name { get; }

            public string Value { get; }

            public OperationCallbackParameter(string name, string value)
            {
                Name = name;
                Value = value;
            }

            public override string ToString()
            {
                return $"{Name};{Value}";
            }
        }

        /// <summary>
        /// Уникальный номер заказа в платёжной системе
        /// </summary>
        [JsonProperty("mdOrder")]
        [JsonPropertyName("mdOrder")]
        public string OrderId { get; set; }

        /// <summary>
        /// Уникальный номер (идентификатор) заказа в системе магазина
        /// </summary>
        [JsonProperty("orderNumber")]
        [JsonPropertyName("orderNumber")]
        public string OrderNumber { get; set; }

        /// <summary>
        /// Аутентификационный код, или контрольная сумма, полученная из набора параметров
        /// </summary>
        [JsonProperty("checksum")]
        [JsonPropertyName("checksum")]
        public string? Checksum { get; set; }

        /// <summary>
        /// Тип операции, о которой пришло уведомление
        /// </summary>
        [JsonProperty("operation")]
        [JsonPropertyName("operation")]
        public CallbackOperationType Operation { get; set; }

        /// <summary>
        /// Индикатор успешности операции, указанной в параметре <see cref="Operation"/>
        /// </summary>
        [JsonProperty("status")]
        [JsonPropertyName("status")]
        public bool Status { get; set; }

        [JsonProperty("amount")]
        [JsonPropertyName("amount")]
        public int? Amount { get; set; }

        protected AlfabankOperationCallback()
        { }

        public AlfabankOperationCallback(string mdOrder, string orderNumber, string operation, int status, int? amount = null, string? checksum = null)
        {
            if (!CallbackOperationType.TryParse(operation, out var operationEnum))
                throw new  ArgumentException($"Invalid operation: {operation}");

            Operation = operationEnum;

            if (status == 0)
                Status = false;
            else if (status == 1)
                Status = true;
            else throw new ArgumentOutOfRangeException(nameof(status));

            OrderId = mdOrder;
            OrderNumber = orderNumber;
            Checksum = checksum;
            Amount = amount;
        }

        public virtual List<OperationCallbackParameter> GetParameters()
        {
            var items = new List<OperationCallbackParameter>()
            {
                new OperationCallbackParameter("mdOrder", OrderId),
                new OperationCallbackParameter("orderNumber", OrderNumber),
                new OperationCallbackParameter("operation", Operation.ToString()),
                new OperationCallbackParameter("status", Status ? "1" : "0")
            };
            if (Amount.HasValue)
                items.Add(new OperationCallbackParameter("amount", Amount.Value.ToString()));

            return items;
        }
    }

    public static class OperationCallbackExtension
    {
        public static string ToString(this IEnumerable<OperationCallbackParameter> parameters)
        {
            return string.Join(";", parameters.OrderBy(x => x.Name));
        }

        public static bool ValidateChecksum(this IEnumerable<OperationCallbackParameter> parameters, string? checksum, Func<string, string> checksumCalc, out string actualChecksum)
        {
            var paramsString = parameters.ToString();
            actualChecksum = checksumCalc(paramsString!);
            return checksum == actualChecksum;
        }

        public static bool ValidateChecksum(this AlfabankOperationCallback callback, Func<string, string> checksumCalc, out string actualChecksum)
        {
            return callback.GetParameters().ValidateChecksum(callback.Checksum, checksumCalc, out actualChecksum);
        }
    }
}
