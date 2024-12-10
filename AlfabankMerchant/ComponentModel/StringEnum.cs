using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AlfabankMerchant.ComponentModel
{
    [DebuggerDisplay("{Value}")]
    public abstract class StringEnum
    {
        public string Value { get; init; }
        
        protected StringEnum(string value)
        {
            Value = value;
        }

        public override bool Equals(object? obj)
            => Value.Equals((obj as StringEnum)?.Value, StringComparison.OrdinalIgnoreCase);
        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;

        public static implicit operator string(StringEnum obj) => obj.Value;
    }

    public abstract class StringEnum<TEnum> : StringEnum
        where TEnum : StringEnum<TEnum>
    {
        protected static Dictionary<string, StringEnum> _registredValues = new(StringComparer.OrdinalIgnoreCase);
        private static bool _isInitialized = false;

        protected StringEnum(string value) : base(value)
        { }

        public static bool Exists(string value)
        {
            EnsureTypeInitialized();
            return _registredValues.ContainsKey(value);
        }

        protected static TEnum RegisterEnum<T>(T value)
            where T : TEnum
        {
            if (value == null)
                throw new ArgumentNullException();
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException();

            if (!_registredValues.ContainsKey(value))
            {
                _registredValues.Add(value.Value, value);
                return value;
            }
            throw new ArgumentException($"Value \"{value}\" already registred");
        }

        public static TEnum Parse(string value)
        {
            if (value == null)
                throw new ArgumentNullException();

            EnsureTypeInitialized();
            if (_registredValues.ContainsKey(value))
                return (TEnum)_registredValues[value];

            throw new ArgumentOutOfRangeException();
        }

        public static bool TryParse(string value, out TEnum res)
        {
            EnsureTypeInitialized();
            if (_registredValues.ContainsKey(value))
            {
                res = (TEnum)_registredValues[value];
                return true;
            }

            res = default!;
            return false;
        }

        public static string ToString(IEnumerable<TEnum> values, string? separator = null)
        {
            return ((IEnumerable<StringEnum>)values).ToString(separator);
        }

        private static void EnsureTypeInitialized()
        {
            if (!_isInitialized)
            {
                RuntimeHelpers.RunClassConstructor(typeof(TEnum).TypeHandle);
                _isInitialized = true;
            }
        }
    }

    public static class StringEnumExtension
    {
        public static string ToString(this IEnumerable<StringEnum>? values, string? separator = null)
        {
            return values != null
                ? string.Join(separator ?? ",", values.Select(x => x.ToString()))
                : "";
        }
    }
}
