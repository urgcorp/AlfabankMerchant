namespace AlfabankMerchant.ComponentModel;

/// <summary>
/// Support values that was not registered at initialization
/// </summary>
public interface INonRegisteredStringEnum<TEnum>
    where TEnum : StringEnum<TEnum>
{
    TEnum ForceParse(string value, out bool registered);
}