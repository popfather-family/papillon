using System.Text.Json;
using System.Text.Json.Serialization;

namespace Papillon.Converters;

internal class SecondsJsonConverter : JsonConverter<Seconds>
{
    public override Seconds Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetInt32();

        return new Seconds(value);
    }

    public override void Write(Utf8JsonWriter writer, Seconds value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}