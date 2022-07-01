using System.Text.Json;
using System.Text.Json.Serialization;

namespace Papillon.Converters;

internal class CountJsonConverter : JsonConverter<Count>
{
    public override Count Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetInt32();

        return new Count(value);
    }

    public override void Write(Utf8JsonWriter writer, Count value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}