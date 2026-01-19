using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AlfabankMerchant.ComponentModel;

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
    protected static readonly Dictionary<string, TEnum> RegisteredValues = new(StringComparer.OrdinalIgnoreCase);
    private static bool _isInitialized = false;

    protected StringEnum(string value) : base(value)
    { }

    public static bool Exists(string value)
    {
        EnsureTypeInitialized();
        return RegisteredValues.ContainsKey(value);
    }

    protected static TEnum RegisterEnum<T>(T value)
        where T : TEnum
    {
        if (value == null)
            throw new ArgumentNullException();
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException();

        if (!RegisteredValues.ContainsKey(value))
        {
            RegisteredValues.Add(value.Value, value);
            return value;
        }
        throw new ArgumentException($"Value \"{value}\" already registered");
    }

    public static TEnum Parse(string value)
    {
        if (value == null)
            throw new ArgumentNullException();

        EnsureTypeInitialized();
        if (RegisteredValues.TryGetValue(value, out var registeredValue))
            return registeredValue;

        throw new ArgumentOutOfRangeException();
    }

    public static bool TryParse(string value, out TEnum res)
    {
        EnsureTypeInitialized();
        if (RegisteredValues.TryGetValue(value, out var registeredValue))
        {
            res = registeredValue;
            return true;
        }

        res = null!;
        return false;
    }

    private static bool findConstructor(System.Reflection.ConstructorInfo ctor)
    {
        var parameters = ctor.GetParameters();
        return parameters.Length == 1 && parameters[0].ParameterType == typeof(string);
    }

    public static TEnum ForceParse(string value, out bool registered)
    {
        EnsureTypeInitialized();
        if (RegisteredValues.TryGetValue(value, out var registeredValue))
        {
            registered = true;
            return registeredValue;
        }
        registered = false;

        var type = typeof(TEnum);
        var ctor = type.GetConstructors()
            .FirstOrDefault(findConstructor);
        if (ctor != null)
            return (TEnum)ctor.Invoke(new object[] { value });

        throw new NotSupportedException($"Type '{type.FullName}' cannot be parsed from string value.");
    }

    public static string ToString(IEnumerable<TEnum> values, string? separator = null)
        => values.ToString(separator);

    private static void EnsureTypeInitialized()
    {
        if (_isInitialized) return;
        lock (RegisteredValues)
        {
            if (_isInitialized) return;
            _isInitialized = true;
            RuntimeHelpers.RunClassConstructor(typeof(TEnum).TypeHandle);
        }
    }
}