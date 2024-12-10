using System.Text.Json;
using System.Text.Json.Serialization;
using AlfabankMerchant.ComponentModel;

namespace AlfabankMerchant.JsonConverter
{
    public class StringEnumConverter<TEnum> : JsonConverter<TEnum>
        where TEnum : StringEnum<TEnum>
    {
        public override TEnum? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (value == null)
                    return null;
                if (StringEnum<TEnum>.TryParse(value, out TEnum enumValue))
                    return enumValue;

                throw new JsonException($"Expect string for {typeof(TEnum).Name}");
            }
            throw new JsonException("Expected string for StringEnum");
        }

        public override void Write(Utf8JsonWriter writer, TEnum stringEnum, JsonSerializerOptions options)
        {
            writer.WriteStringValue(stringEnum.Value);
        }
    }
}
