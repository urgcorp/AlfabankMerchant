using AlfabankMerchant.ComponentModel;
using AlfabankMerchant.Test.Common;

namespace AlfabankMerchant.Test;

public class StringEnumTest
{
    [Fact]
    public void StringEnum_ParseNullThrowException()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            TestStringEnum.Parse(null!);
        });
    }

    [Fact]
    public void StringEnum_ParseUnregisteredThrowException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            TestStringEnum.Parse("XXXTENTACION");
        });
    }

    [Fact]
    public void StringEnum_ToString()
    {
        var strVal1 = TestStringEnum.One.ToString();
        var strVal2 = TestStringEnum.Two.ToString();
        var strVal3 = TestStringEnum.Three.ToString();
        
        Assert.Equal(TestStringEnum.One.Value, strVal1);
        Assert.Equal(TestStringEnum.Two.Value, strVal2);
        Assert.Equal(TestStringEnum.Three.Value, strVal3);
    }
    
    [Theory]
    [InlineData("one")]
    [InlineData("ONE")]
    [InlineData("One")]
    public void StringEnum_ExistsCaseInsensitive(string value)
    {
        Assert.True(TestStringEnum.Exists(value));
    }
    
    [Theory]
    [InlineData("one")]
    [InlineData("ONE")]
    [InlineData("One")]
    public void StringEnum_ParseRegistered(string value)
    {
        var e = TestStringEnum.Parse(value);
        Assert.Equal(TestStringEnum.One, e);
    }

    [Theory]
    [InlineData("one")]
    [InlineData("ONE")]
    [InlineData("One")]
    public void StringEnum_Generic_TryParseRegistered(string value)
    {
        var parsed = StringEnum<TestStringEnum>.TryParse(value, out var res);
        Assert.True(parsed);
        Assert.Equal(TestStringEnum.One, res);
    }
    [Fact]
    public void StringEnum_Enumerable_EmptyToString()
    {
        TestStringEnum[] arr = Array.Empty<TestStringEnum>();
        var strVal = arr.ToString(null);
        Assert.Equal(string.Empty, strVal);
    }
    
    [Fact]
    public void StringEnum_Enumerable_SingleToString()
    {
        var arr = new[] { TestStringEnum.One };
        var strVal = arr.ToString(null);
        Assert.Equal(TestStringEnum.One.Value, strVal);
    }

    [Fact]
    public void StringEnum_Enumerable_MultipleUniqueToStrings()
    {
        var arr = new[] { TestStringEnum.Two, TestStringEnum.Three };
        var strVal = arr.ToString(null);
        var stringNames = strVal!.Split(',');
        Assert.Equal(2, stringNames.Length);
        for (int i = 0; i < stringNames.Length; i++)
        {
            var parsedEnum = TestStringEnum.Parse(stringNames[i]);
            Assert.Equal(arr[i], parsedEnum);
        }
    }

    [Fact]
    public void StringEnum_Enumerable_MultipleDuplicatedToStrings()
    {
        var arr = new[] { TestStringEnum.Two, TestStringEnum.Three, TestStringEnum.Two, TestStringEnum.Two, TestStringEnum.Three };
        var strVal = arr.ToString(null);
        var stringNames = strVal!.Split(',');
        Assert.Equal(5, stringNames.Length);
        for (int i = 0; i < stringNames.Length; i++)
        {
            var parsedEnum = TestStringEnum.Parse(stringNames[i]);
            Assert.Equal(arr[i], parsedEnum);
        }
    }

    [Fact]
    public void StringEnum_Enumerable_CustomSeparatorToString()
    {
        string separator = "|";
        var arr = new[] { TestStringEnum.Two, TestStringEnum.Three };
        var strVal = arr.ToString(separator);
        var stringNames = strVal!.Split(separator);
        Assert.Equal(2, stringNames.Length);
        for (int i = 0; i < stringNames.Length; i++)
        {
            var parsedEnum = TestStringEnum.Parse(stringNames[i]);
            Assert.Equal(arr[i], parsedEnum);
        }
    }
}