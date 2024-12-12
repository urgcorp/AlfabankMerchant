using Newtonsoft.Json;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.JsonConverter.Newtonsoft
{
    public class StringEnumConverter<TEnum> : JsonConverter<TEnum>
        where TEnum : StringEnum<TEnum>
    {
        public override TEnum? ReadJson(JsonReader reader, Type objectType, TEnum? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value is string val && val != null)
            {
                if (StringEnum<TEnum>.TryParse(val, out TEnum enumValue))
                    return enumValue;
                throw new JsonException($"Expect string for {typeof(TEnum).Name}");
            }
            throw new JsonException("Expected string for StringEnum");
        }

        public override void WriteJson(JsonWriter writer, TEnum? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteValue(value.Value);
        }
    }
}
