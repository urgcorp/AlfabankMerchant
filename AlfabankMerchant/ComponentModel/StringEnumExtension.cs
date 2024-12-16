using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant;

public static class StringEnumExtension
{
    public static string ToString(this IEnumerable<StringEnum>? values, string? separator)
    {
        return values != null
            ? string.Join(separator ?? ",", values.Select(x => x.ToString()))
            : "";
    }
}
