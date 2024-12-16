using AlfabankMerchant.ComponentModel;
using AlfabankMerchant.JsonConverter;

namespace AlfabankMerchant.Test.Common;

[Newtonsoft.Json.JsonConverter(typeof(JsonConverter.Newtonsoft.StringEnumConverter<TestStringEnum>))]
[System.Text.Json.Serialization.JsonConverter(typeof(StringEnumConverter<TestStringEnum>))]
public class TestStringEnum : StringEnum<TestStringEnum>
{
    private TestStringEnum(string value) : base(value)
    { } 
        
    public static readonly TestStringEnum One = RegisterEnum(new TestStringEnum("one"));
        
    public static readonly TestStringEnum Two = RegisterEnum(new TestStringEnum("Two"));
        
    public static readonly TestStringEnum Three = RegisterEnum(new TestStringEnum("THREE"));
}