using System.Reflection;
using System.Runtime.InteropServices;

namespace Papillon.DI.Microsoft.Reflection;

internal static class MetadataLoadContextFactory
{
    private static string RuntimeDirectory => RuntimeEnvironment.GetRuntimeDirectory();

    private static AssemblyName CoreAssemblyName => typeof(object).Assembly.GetName();

    internal static MetadataLoadContext Create(IEnumerable<string> assemblyPaths)
    {
        var resolver = CreatePathAssemblyResolver(assemblyPaths);

        return new MetadataLoadContext(resolver, CoreAssemblyName.Name);
    }

    private static PathAssemblyResolver CreatePathAssemblyResolver(IEnumerable<string> assemblyPaths)
    {
        assemblyPaths = Directory.GetFiles(RuntimeDirectory, "*dll")
                                 .Union(assemblyPaths);

        return new PathAssemblyResolver(assemblyPaths);
    }
}