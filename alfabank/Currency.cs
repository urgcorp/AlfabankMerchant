using System.Diagnostics;
using alfabank.ComponentModel;

namespace alfabank
{
    [DebuggerDisplay("{Value} ({CurrencyCode})")]
    public sealed class Currency : StringEnum<Currency>
    {
        private static readonly Dictionary<int, Currency> _currencies = new Dictionary<int, Currency>();

        /// <summary>
        /// Код валюты платежа ISO 4217
        /// </summary>
        public int CurrencyCode { get; private set; }

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
            var cur = RegisterEnum(value);
            cur.CurrencyCode = code;
            _currencies[code] = cur;
            return cur;
        }

        public static bool Exists(int currencyCode)
            => _currencies.ContainsKey(currencyCode);

        public static Currency Parse(int currencyCode)
        {
            if (!_currencies.ContainsKey(currencyCode))
                throw new ArgumentOutOfRangeException();

            return _currencies[currencyCode];
        }

        public static bool TryParse(int currencyCode, out Currency currency)
        {
            if (Exists(currencyCode))
            {
                currency = _currencies[currencyCode];
                return true;
            }

            currency = default!;
            return false;
        }

        public static new Currency Parse(string value)
        {
            if (value == null)
                throw new ArgumentNullException();

            if (_registredValues.ContainsKey(value))
                return _registredValues[value];

            if (int.TryParse(value, out int currencyCode) && Exists(currencyCode))
                return _currencies[currencyCode];

            throw new ArgumentOutOfRangeException();
        }

        public static new bool TryParse(string value, out Currency currency)
        {
            if (Exists(value))
            {
                currency = _registredValues[value];
                return true;
            }

            if (int.TryParse(value, out int currencyCode) && Exists(currencyCode))
            {
                currency = _currencies[currencyCode];
                return true;
            }

            currency = default!;
            return false;
        }

        public static implicit operator int(Currency curr)
            => curr.CurrencyCode;
    }
}
