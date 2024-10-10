﻿using System.Collections;
using System.Reflection;

namespace alfabank.ComponentModel
{
    public abstract class AlfabankAction
    {
        public abstract string Action { get; set; }

        public Dictionary<string, string> GetActionQueryParams()
        {
            var queryParams = new Dictionary<string, string>();

            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var property in properties)
            {
                var propAttr = property.GetCustomAttribute<ActionPropertyAttribute>();
                if (propAttr != null)
                {
                    var value = property.GetValue(this);
                    if (value != null)
                    {
                        if (value is string)
                            queryParams[propAttr.Name] = (string)value;
                        else
                            queryParams[propAttr.Name] = value.ToString()!;
                    }
                }
            }

            return queryParams;
        }

        public bool ValidateActionParams(out Dictionary<string, string> errors)
        {
            var res = new Dictionary<string, string>();
            var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var property in properties)
            {
                var propAttr = property.GetCustomAttribute<ActionPropertyAttribute>();
                if (propAttr != null)
                {
                    var value = property.GetValue(this);
                    if (propAttr.Required)
                    {
                        if (value == null)
                        {
                            // Required must never be null
                            res.Add(property.Name, $"Require value for \"{propAttr.Name}\"");
                        }
                        else if (value is string stringVal && string.IsNullOrEmpty(stringVal) && !propAttr.AllowEmpty)
                        {
                            // Required check if allow empty for values
                            res.Add(property.Name, $"Require value for \"{propAttr.Name}\"");
                        }
                        else if (value is IEnumerable enumerable && !enumerable.Cast<object>().Any() && !propAttr.AllowEmpty)
                        {
                            // Required check if allow empty for enumerable
                            res.Add(property.Name, $"Require value for \"{propAttr.Name}\"");
                        }
                    }

                    // TODO: Проверка типа значения
                }
            }

            errors = res;
            return !res.Any();
        }

        [ActionProperty("userName", Type = "AN..30")]
        public string? Login { get; set; }

        [ActionProperty("password", Type = "AN..30")]
        public string? Password { get; set; }

        /// <summary>
        /// Открытый ключ, который можно использовать для регистрации заказа.
        /// <para>Если для аутентификации при регистрации заказа используются логин и пароль, параметр token передавать не нужно</para>
        /// </summary>
        [ActionProperty("token", Type = "AN..30")]
        public string? Token { get; set; }

        /// <summary>
        /// yyyyMMddHHmmss DateTime format
        /// </summary>
        protected static string? GetDTParamValue(DateTime? val) => val.HasValue ? val.Value.ToString(AlfaBankConfiguration.REQUEST_SEND_DATETIME_FORMAT) : null;

        protected static string? GetBoolParamVal(bool? val) => val.HasValue ? val.Value ? "true" : "false" : null;

        protected static string? GetJsonParamsString(Dictionary<string, string>? jsonParams) => jsonParams != null
            ? $"{{{string.Join(",", jsonParams.Select(p => $"\"{p.Key}\":\"{p.Value}\""))}}}"
            : null;

        protected static string? TryGetParamValue(Dictionary<string, string>? paramsDict, string paramKey)
        {
            if (paramsDict != null && paramsDict.ContainsKey(paramKey))
                return paramsDict[paramKey];
            return null;
        }

        protected static Dictionary<string, string>? TrySetParamValue(Dictionary<string, string>? paramsDict, string paramKey, string? paramVal)
        {
            if (string.IsNullOrEmpty(paramKey))
                throw new ArgumentException();

            if (paramVal == null)
            {
                if (paramsDict != null)
                {
                    if (paramsDict.ContainsKey("subscriptionPurpose") && paramsDict.Count > 1)
                        paramsDict.Remove("subscriptionPurpose");
                    else
                        return null;
                }
            }
            else
            {
                if (paramsDict != null)
                    paramsDict.Add(paramKey, paramVal);
                else
                    return new Dictionary<string, string>() { { paramKey, paramVal } };
            }

            return paramsDict;
        }
    }

    public abstract class AlfabankAction<TResp> : AlfabankAction
        where TResp : class
    { }
}
