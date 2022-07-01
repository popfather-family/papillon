using System.Text.Json;
using System.Text.Json.Serialization;

namespace Papillon.Converters;

internal class DaysJsonConverter : JsonConverter<Days>
{
    public override Days Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetInt32();

        return new Days(value);
    }

    public override void Write(Utf8JsonWriter writer, Days value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}