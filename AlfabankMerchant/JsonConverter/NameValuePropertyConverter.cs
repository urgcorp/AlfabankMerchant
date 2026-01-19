using System.Text.Json;
using System.Text.Json.Serialization;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.JsonConverter;

public class NameValuePropertyConverter : JsonConverter<Dictionary<string, string>>
{
    public override Dictionary<string, string>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected StartArray token");

        var result = new Dictionary<string, string>();
        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            var element = JsonSerializer.Deserialize<NameValueProperty>(ref reader, options);
            result[element.Name] = element.Value;
        }
        return result;
    }

    public override void Write(Utf8JsonWriter writer, Dictionary<string, string>? value, JsonSerializerOptions? options)
    {
        if (value != null)
        {
            var nameValueList = value?.Select(kvp => new NameValueProperty(kvp.Key, kvp.Value)).ToList();
            JsonSerializer.Serialize(writer, nameValueList, options);
        }
    }
}