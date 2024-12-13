namespace AlfabankMerchant.ComponentModel
{
    /// <summary>
    /// Support values that was not registred at initialization
    /// </summary>
    public interface INonRegistredStringEnum<TEnum>
        where TEnum : StringEnum<TEnum>
    {
        TEnum ForceParse(string value, out bool registred);
    }
}
