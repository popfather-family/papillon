using System.Text.Json;
using System.Text.Json.Serialization;
using Papillon.Reflection;

namespace Papillon.Text.Json;

public static class JsonSerializerOptionsExtensions
{
    public static void AddJsonConverters(this JsonSerializerOptions options)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        var converterTypes = TypeFinder.Find<JsonConverter>(assemblies);
        foreach (var converterType in converterTypes)
        {
            if (Activator.CreateInstance(converterType) is not JsonConverter converter)
            {
                continue;
            }

            options.Converters.Add(converter);
        }
    }
}