using System.Collections.Generic;
using System.Linq;
using AlfabankMerchant.ComponentModel;

// ReSharper disable once CheckNamespace
namespace AlfabankMerchant
{
    public static class StringEnumExtension
    {
        public static string ToString(this IEnumerable<StringEnum>? values, string? separator)
        {
            return values != null
                ? string.Join(separator ?? ",", values.Select(x => x.ToString()))
                : "";
        }
    }
}
