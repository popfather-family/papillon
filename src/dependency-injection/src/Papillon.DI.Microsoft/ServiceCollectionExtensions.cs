using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Papillon.DI.Microsoft.Reflection;

namespace Papillon.DI.Microsoft;

internal static class ServiceCollectionExtensions
{
    public static void AddModules(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (assemblies.Length == 0)
        {
            return;
        }

        var types = assemblies.SelectMany(c => c.GetTypes())
                              .Where(c => c.AnnotatedBy(typeof(ContainerBuilderAttribute)));

        foreach (var type in types)
        {
            ConfigurationMethodInvoker.InvokeAll(type, services);
        }
    }
}