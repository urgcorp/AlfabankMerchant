using Newtonsoft.Json;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.JsonConverter.Newtonsoft;

public class NameValuePropertyConverter : JsonConverter<Dictionary<string, string>>
{
    public override Dictionary<string, string>? ReadJson(JsonReader reader, Type objectType, Dictionary<string, string>? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        // Чтение JSON-массива и его преобразование в Dictionary
        var nameValueList = serializer.Deserialize<List<NameValueProperty>>(reader);
        return nameValueList?.ToDictionary(item => item.Name, item => item.Value) ?? new Dictionary<string, string>();
    }

    public override void WriteJson(JsonWriter writer, Dictionary<string, string>? value, JsonSerializer serializer)
    {
        // Преобразование Dictionary в JSON-массив объектов NameValueProperty
        if (value != null)
        {
            var nameValueList = value?.Select(kvp => new NameValueProperty(kvp.Key, kvp.Value)).ToList();
            serializer.Serialize(writer, nameValueList);
        }
    }
}