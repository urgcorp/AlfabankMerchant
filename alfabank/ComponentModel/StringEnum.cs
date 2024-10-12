using System;
using System.Diagnostics;

namespace alfabank.ComponentModel
{
    [DebuggerDisplay("{Value}")]
    public abstract class StringEnum
    {
        public string Value { get; protected set; } = null!;

        public override bool Equals(object? obj)
            => Value.Equals((obj as StringEnum)?.Value, StringComparison.OrdinalIgnoreCase);
        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;

        public static implicit operator string(StringEnum obj) => obj.Value;
    }

    public abstract class StringEnum<TEnum> : StringEnum
        where TEnum : StringEnum<TEnum>, new()
    {
        protected static Dictionary<string, TEnum> _registredValues = new(StringComparer.OrdinalIgnoreCase);

        protected StringEnum()
        { }

        protected static TEnum RegisterEnum(string value)
        {
            if (value == null)
                throw new ArgumentNullException();
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException();

            if (!_registredValues.ContainsKey(value))
            {
                var newEnum = new TEnum() { Value = value };
                _registredValues.Add(value, newEnum);
                return newEnum;
            }
            throw new ArgumentException($"Value \"{value}\" already registred");
        }

        public static bool Exists(string value)
            => _registredValues.ContainsKey(value);

        public static TEnum Parse(string value)
        {
            if (value == null)
                throw new ArgumentNullException();

            if (_registredValues.ContainsKey(value))
                return _registredValues[value];

            throw new ArgumentOutOfRangeException();
        }

        public static bool TryParse(string value, out TEnum res)
        {
            if (Exists(value))
            {
                res = _registredValues[value];
                return true;
            }

            res = default!;
            return false;
        }

        public static string ToString(IEnumerable<TEnum> values, string? separator = null)
        {
            return ((IEnumerable<StringEnum>)values).ToString(separator);
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
