using System.Reflection;

namespace Papillon.DI.Microsoft.Reflection;

internal static class AssemblyHelper
{
    private static AppDomain CurrentDomain => AppDomain.CurrentDomain;

    private static AssemblyName PapillonAssemblyName => typeof(InjectableAttribute).Assembly.GetName();

    internal static Assembly[] LoadReferencedAssemblies()
    {
        var assemblyNames = GetReferencedAssemblyNames();

        return assemblyNames.Select(assemblyName => CurrentDomain.Load(assemblyName))
                            .ToArray();
    }

    private static IEnumerable<AssemblyName> GetReferencedAssemblyNames()
    {
        var assemblyPaths = Directory.GetFiles(CurrentDomain.BaseDirectory, "*.dll");
        using var metadataContext = MetadataLoadContextFactory.Create(assemblyPaths);

        foreach (var assemblyPath in assemblyPaths)
        {
            var assembly = metadataContext.LoadFromAssemblyPath(assemblyPath);
            if (assembly.HasReferenceToPapillon())
            {
                yield return assembly.GetName();
            }
        }
    }

    private static bool HasReferenceToPapillon(this Assembly assembly)
    {
        return assembly.GetReferencedAssemblies()
                       .Any(c => c.Name == PapillonAssemblyName.Name);
    }
}