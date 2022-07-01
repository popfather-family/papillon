using System.Text.Json;
using System.Text.Json.Serialization;

namespace Papillon.Converters;

internal class MinutesJsonConverter : JsonConverter<Minutes>
{
    public override Minutes Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetInt32();

        return new Minutes(value);
    }

    public override void Write(Utf8JsonWriter writer, Minutes value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}