using System.Diagnostics;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.Common;

[DebuggerDisplay("{Value} ({CurrencyCode})")]
[Newtonsoft.Json.JsonConverter(typeof(JsonConverter.Newtonsoft.StringEnumConverter<Currency>))]
[System.Text.Json.Serialization.JsonConverter(typeof(JsonConverter.StringEnumConverter<Currency>))]
public class Currency : StringEnum<Currency>
{
    private static readonly Dictionary<int, Currency> _currencies = new();

    /// <summary>
    /// Код валюты платежа ISO 4217
    /// </summary>
    public int CurrencyCode { get; private set; }
        
    private Currency(string value, int currencyCode) : base(value)
    {
        CurrencyCode = currencyCode;
    }

    /// <summary>
    /// Российский рубль до деноминации 1998 года;
    /// </summary>
    public static readonly Currency RUR = RegisterCurrency("RUR", 810);

    /// <summary>
    /// Российский рубль после деноминации 1998 года;
    /// </summary>
    public static readonly Currency RUB = RegisterCurrency("RUB", 643);

    /// <summary>
    /// Белорусский рубль с 2016 года
    /// </summary>
    public static readonly Currency BYN = RegisterCurrency("BYN", 933);

    private static Currency RegisterCurrency(string value, int code)
    {
        var cur = RegisterEnum(new Currency(value, code));
        cur.CurrencyCode = code;
        _currencies[code] = cur;
        return cur;
    }

    /// <summary>
    /// Check if currency with this currency code is registered as supported
    /// </summary>
    /// <param name="currencyCode">Numeric currency code</param>
    public static bool Exists(int currencyCode)
        => _currencies.ContainsKey(currencyCode);

    public static Currency Parse(int currencyCode)
    {
        if (!_currencies.TryGetValue(currencyCode, out var currency))
            throw new ArgumentOutOfRangeException();

        return currency;
    }

    public static bool TryParse(int currencyCode, out Currency currency)
    {
        if (Exists(currencyCode))
        {
            currency = _currencies[currencyCode];
            return true;
        }

        currency = null!;
        return false;
    }

    public new static Currency Parse(string value)
    {
        if (value == null)
            throw new ArgumentNullException();

        if (RegisteredValues.TryGetValue(value, out var registeredValue))
            return registeredValue;

        if (int.TryParse(value, out var currencyCode) && Exists(currencyCode))
            return _currencies[currencyCode];

        throw new ArgumentOutOfRangeException();
    }

    public new static bool TryParse(string value, out Currency currency)
    {
        if (Exists(value))
        {
            currency = RegisteredValues[value];
            return true;
        }

        if (int.TryParse(value, out var currencyCode) && Exists(currencyCode))
        {
            currency = _currencies[currencyCode];
            return true;
        }

        currency = null!;
        return false;
    }

    /// <summary>
    /// Cast currency to currency code
    /// </summary>
    /// <returns>Numeric currency code</returns>
    public static implicit operator int(Currency curr)
        => curr.CurrencyCode;
}