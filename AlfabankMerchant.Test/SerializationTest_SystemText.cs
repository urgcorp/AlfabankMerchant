using System.Text.Json;
using System.Text.Json.Nodes;
using AlfabankMerchant.Test.Common;

namespace AlfabankMerchant.Test;

public class SerializationTest_SystemText
{
    [Fact]
    public void StringEnum_SerializeObjectWithEnum()
    {
        var model = new StringEnumWrap();
        var json = JsonSerializer.Serialize(model);
        var jObj = (JsonObject)JsonNode.Parse(json)!;
        
        Assert.True(jObj.ContainsKey("single"));
        Assert.True(jObj.ContainsKey("multiple"));
        
        Assert.Equal(model.Single.Value, jObj["single"]?.GetValue<string>());
        var multiple = jObj["multiple"];
        Assert.IsType<JsonArray>(multiple);
        
        JsonArray multipleArr = (JsonArray)jObj["multiple"]!;
        for (var i = 0; i < multipleArr!.Count; i++)
        {
            Assert.Equal(model.Multiple.ElementAt(i).Value, multipleArr!.ElementAt(i)!.GetValue<string>());
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
        var obj = JsonSerializer.Deserialize<BooleanStringWrap>(json);
        Assert.Equal(expected, obj!.BValue);
    } 

    [Theory]
    [InlineData("")]
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
            JsonSerializer.Deserialize<BooleanStringWrap>(json);
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
        
        var json = JsonSerializer.Serialize(obj);
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
        
        var json = JsonSerializer.Serialize(model);
        var jObj = (JsonObject)JsonNode.Parse(json)!;
        Assert.True(jObj.ContainsKey("kvAttr"));
        
        Assert.IsType<JsonArray>(jObj["kvAttr"]);
        foreach (JsonObject kvElement in jObj["kvAttr"]!.AsArray())
        {
            Assert.True(kvElement!.ContainsKey("name"));
            Assert.True(kvElement!.ContainsKey("value"));
            
            var keyProp = kvElement["name"];
            Assert.True(keyProp is JsonValue);
            Assert.True(data.ContainsKey(keyProp!.ToString()));
            var valProp = kvElement["value"];
            Assert.True(valProp is JsonValue);
            Assert.Equal(data[keyProp!.ToString()], valProp!.ToString());
        }
    }

    [Fact]
    public void NameValuePropertyConverter_WriteNull()
    {
        var obj = new NameValuePropertyWrap()
        {
            Attributes = null
        };
        
        var json = JsonSerializer.Serialize(obj);
        var jObj = (JsonObject)JsonNode.Parse(json)!;
        Assert.True(jObj.ContainsKey("kvAttr"));
    }
}