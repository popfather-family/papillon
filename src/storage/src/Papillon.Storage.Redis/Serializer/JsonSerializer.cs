using System.Text.Json;
using Foundatio.Serializer;
using Papillon.Text.Json;
using SystemJsonSerializer = System.Text.Json.JsonSerializer;

namespace Papillon.Storage.Redis.Serializer;

public class JsonSerializer : ITextSerializer
{
    private readonly JsonSerializerOptions options = new();

    public JsonSerializer()
    {
        options.AddJsonConverters();
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    }

    public void Serialize(object data, Stream outputStream)
    {
        var writer = new Utf8JsonWriter(outputStream);
        var inputType = data.GetType();
        SystemJsonSerializer.Serialize(writer, data, inputType, options);
        writer.Flush();
    }

    public object? Deserialize(Stream inputStream, Type objectType)
    {
        using var reader = new StreamReader(inputStream);
        var json = reader.ReadToEnd();

        return SystemJsonSerializer.Deserialize(json, objectType, options);
    }
}