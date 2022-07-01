using System.Text.Json;
using System.Text.Json.Serialization;

namespace Papillon.Converters;

internal class LongCountJsonConverter : JsonConverter<LongCount>
{
    public override LongCount Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetInt64();

        return new LongCount(value);
    }

    public override void Write(Utf8JsonWriter writer, LongCount value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}