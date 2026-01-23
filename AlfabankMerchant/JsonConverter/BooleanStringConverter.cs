using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AlfabankMerchant.JsonConverter
{
    public class BooleanStringConverter : JsonConverter<bool?>
    {
        public override bool? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (value == null)
                    return null;
                if (value.Equals("true", StringComparison.OrdinalIgnoreCase))
                    return true;
                if (value.Equals("false", StringComparison.OrdinalIgnoreCase))
                    return false;

                throw new JsonException("Expected values are 'true', 'false' or null.");
            }
            throw new JsonException("Expected string value.");
        }

        public override void Write(Utf8JsonWriter writer, bool? value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case true:
                    writer.WriteStringValue("true");
                    break;
                case false:
                    writer.WriteStringValue("false");
                    break;
                default:
                    writer.WriteNullValue();
                    break;
            }
        }
    }
}