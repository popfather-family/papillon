using System.Text.Json;
using System.Text.Json.Serialization;

namespace Papillon.Converters;

internal class HoursJsonConverter : JsonConverter<Hours>
{
    public override Hours Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetInt32();

        return new Hours(value);
    }

    public override void Write(Utf8JsonWriter writer, Hours value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}