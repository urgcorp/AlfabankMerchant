using Newtonsoft.Json;

namespace AlfabankMerchant.JsonConverter.Newtonsoft
{
    public class BooleanStringConverter : JsonConverter<bool?>
    {
        public override bool? ReadJson(JsonReader reader, Type objectType, bool? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return null;
            
            if (reader.Value is string val)
            {
                if (val.Equals("true", StringComparison.OrdinalIgnoreCase))
                    return true;
                if (val.Equals("false", StringComparison.OrdinalIgnoreCase))
                    return false;

                throw new JsonException("Expected values are 'true', 'false' or null.");
            }
            throw new JsonException("Expected string value.");
        }

        public override void WriteJson(JsonWriter writer, bool? value, JsonSerializer serializer)
        {
            switch (value)
            {
                case true:
                    writer.WriteValue("true");
                    break;
                case false:
                    writer.WriteValue("false");
                    break;
                default:
                    writer.WriteNull();
                    break;
            }
        }
    }
}
