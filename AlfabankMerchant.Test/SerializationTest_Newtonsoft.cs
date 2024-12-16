using AlfabankMerchant.Test.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AlfabankMerchant.Test;

public class SerializationTest_Newtonsoft
{
    [Fact]
    public void StringEnum_SerializeObjectWithEnum()
    {
        var obj = new StringEnumWrap();
        var json = JsonConvert.SerializeObject(obj);
        var jObj = JObject.Parse(json);
        var single = jObj?["single"];
        Assert.Equal(obj.Single.Value, single?.Value<string>());

        var multiple = jObj?["multiple"];
        var multipleArr = jObj?["multiple"]?.ToArray();
        Assert.NotNull(multiple);
        for (var i = 0; i < multipleArr!.Length; i++)
        {
            Assert.Equal(obj.Multiple.ElementAt(i).Value, multipleArr!.ElementAt(i)!.Value<string>());
        }
    }
    
    [Theory]
    [InlineData(null, null)]
    [InlineData("true", true)]
    [InlineData("True", true)]
    [InlineData("TRUE", true)]
    [InlineData("false", false)]
    [InlineData("False", false)]
    [InlineData("FALSE", false)]
    public void BooleanStringConverter_ReadCorrectValue(string? value, bool? expected)
    {
        string json = BooleanStringWrap.FormatJsonObject(value);
        var obj = JsonConvert.DeserializeObject<BooleanStringWrap>(json);
        Assert.Equal(expected, obj!.BValue);
    } 

    [Theory]
    [InlineData("")]
    [InlineData("null")]
    [InlineData("1")]
    [InlineData("0")]
    [InlineData("\"true\"")]
    [InlineData("'")]
    [InlineData("\"")]
    [InlineData("rnd")]
    public void BooleanStringConverter_ThrowIncorrectValues(string value)
    {
        string json = BooleanStringWrap.FormatJsonObject(value);
        Assert.Throws<JsonException>(() =>
        {
            JsonConvert.DeserializeObject<BooleanStringWrap>(json);
        });
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData(true)]
    [InlineData(false)]
    public void BooleanStringConverter_Write(bool? value)
    {
        var obj = new BooleanStringWrap()
        {
            BValue = value
        };
        
        var json = JsonConvert.SerializeObject(obj);
        if (BooleanStringWrap.FindPropValueFromJson(json, out var outerProperty))
        {
            switch (outerProperty)
            {
                case "\"true\"":
                    Assert.True(value);
                    return;
                case "\"false\"":
                    Assert.False(value);
                    return;
                case "null":
                    Assert.Null(value);
                    return;
            }
        }
        Assert.True(false);
    }

    [Fact]
    public void NameValuePropertyConverter_WriteCollection()
    {
        var data = new Dictionary<string, string>()
        {
            { "key", "value" },
            { "key2", "value2" }
        };
        var model = new NameValuePropertyWrap()
        {
            Attributes = data
        };
        
        var json = JsonConvert.SerializeObject(model);
        var jObj = JObject.Parse(json);
        Assert.True(jObj.ContainsKey("kvAttr"));
        var kvJObj = jObj["kvAttr"];
        Assert.Equal(JTokenType.Array, kvJObj!.Type);
        JArray jObjArr = (JArray)kvJObj;
        Assert.Equal(data.Count, jObjArr.Count);
        foreach (var attrObj in jObjArr)
        {
            var keyProp = attrObj["name"];
            Assert.NotNull(keyProp);
            var valProp = attrObj["value"];
            Assert.NotNull(valProp);
                
            var key = keyProp!.Value<string>();
            Assert.True(data.ContainsKey(key!));
            var val = valProp!.Value<string>();
            Assert.Equal(data[key!], val);
        }
    }

    [Fact]
    public void NameValuePropertyConverter_WriteNull()
    {
        var obj = new NameValuePropertyWrap()
        {
            Attributes = null
        };
        
        var json = JsonConvert.SerializeObject(obj);
        var jObj = JObject.Parse(json);
        Assert.True(jObj.ContainsKey("kvAttr"));
        var kvJObj = jObj["kvAttr"];
        Assert.Equal(JTokenType.Null, kvJObj!.Type);
    }
}