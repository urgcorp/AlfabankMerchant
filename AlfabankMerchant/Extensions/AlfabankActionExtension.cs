using System.Collections;
using System.Reflection;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant;

public static class AlfabankActionExtension
{
    /// <summary>
    /// <para>Find URL path defined within action</para>
    /// <para>Returns <see cref="AlfabankAction.ActionUrl"/> if defined</para>
    /// </summary>
    /// <param name="clientType">Type of client which expects defined URL</param>
    /// <returns>URL defined in action</returns>
    public static string? FindActionUrl(this AlfabankAction action, string clientType)
    {
        if (!string.IsNullOrEmpty(action.ActionUrl))
            return action.ActionUrl;
        if (string.IsNullOrEmpty(clientType))
            throw new ArgumentException(nameof(clientType));

        var actionAttribute = (ActionUrlAttribute?)Attribute.GetCustomAttribute(action.GetType(), typeof(ActionUrlAttribute));
        if (actionAttribute != null && actionAttribute.ClientType.Equals(clientType, StringComparison.OrdinalIgnoreCase))
            return actionAttribute.Url;
            
        return null;
    }
    
    private static readonly BindingFlags actionParamsBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
    
    /// <summary>Extract query parameters from action</summary>
    /// <returns>Set of parameters to send with HTTP request</returns>
    public static Dictionary<string, string?> GetActionParams(this AlfabankAction action)
    {
        var queryParams = new Dictionary<string, string?>();
        foreach (var kvp in action.Extensions)
        {
            if (kvp.Value == null)
                queryParams.Add(kvp.Key, null);
            else if (kvp.Value is string strVal)
                queryParams.Add(kvp.Key, strVal);
            else
                queryParams.Add(kvp.Key, kvp.Value.ToString());
        }
        
        var properties = action.GetType().GetProperties(actionParamsBindingFlags);
        foreach (var property in properties)
        {
            var propAttr = property.GetCustomAttribute<ActionPropertyAttribute>();
            if (propAttr != null)
            {
                var value = property.GetValue(action);
                if (value != null)
                {
                    if (value is string valueStr)
                        queryParams[propAttr.Name] = valueStr;
                    else
                        queryParams[propAttr.Name] = value.ToString();
                }
            }
        }

        return queryParams;
    }
    
    /// <summary>
    /// Validate action properties that have <see cref="ActionPropertyAttribute"/> constraints
    /// </summary>
    /// <param name="errors">Properties that have validation errors</param>
    /// <param name="ignore">
    /// <para>Names of properties to ignore validation</para>
    /// <para>Names are case-sensitive</para>
    /// </param>
    /// <returns>Indicates if any validation errors</returns>
    public static bool ValidateActionParams(this AlfabankAction action, out Dictionary<string, string> errors, ICollection<string>? ignore = null)
        {
            errors = new Dictionary<string, string>();
            var properties = action.GetType().GetProperties(actionParamsBindingFlags);
            foreach (var property in properties)
            {
                var propAttr = property.GetCustomAttribute<ActionPropertyAttribute>();
                if (propAttr != null)
                {
                    if (ignore != null && ignore.Contains(propAttr.Name, StringComparer.Ordinal))
                        continue;

                    var value = property.GetValue(action);
                    if (propAttr.Required)
                    {
                        if (value == null)
                        {
                            // Required must never be null
                            errors.Add(property.Name, $"Require value for \"{propAttr.Name}\"");
                        }
                        else if (value is string stringVal && string.IsNullOrEmpty(stringVal) && !propAttr.AllowEmpty)
                        {
                            // Required check if allow empty for values
                            errors.Add(property.Name, $"Require value for \"{propAttr.Name}\"");
                        }
                        else if (value is IEnumerable enumerable && !enumerable.Cast<object>().Any() && !propAttr.AllowEmpty)
                        {
                            // Required check if allow empty for enumerable
                            errors.Add(property.Name, $"Require value for \"{propAttr.Name}\"");
                        }
                    }

                    // TODO: Проверка типа значения
                }
            }
            return !errors.Any();
        }
}