using System.Collections;
using Microsoft.Extensions.DependencyInjection;
using Papillon.DI.Microsoft.Reflection;

namespace Papillon.DI.Microsoft;

internal static class ConfigurationMethodInvoker
{
    public static void InvokeAll(Type type, IServiceCollection services)
    {
        var methods = type.GetInvocationMethods(services);
        foreach (var method in methods)
        {
            method.TryInvoke();
        }
    }

    private static IEnumerable<Action> GetInvocationMethods(this Type type, IServiceCollection services)
    {
        yield return () => { type.InvokeByCreateInstance(services); };
        yield return () => { type.InvokeAsStaticMethod(services); };
    }

    private static void InvokeByCreateInstance(this Type type, IEnumerable services)
    {
        var instance = Activator.CreateInstance(type)!;
        type.InvokeConfigurationMethods(instance, services);
    }

    private static void InvokeAsStaticMethod(this Type type, IEnumerable services)
    {
        type.InvokeConfigurationMethods(null, services);
    }

    private static void InvokeConfigurationMethods(this Type type, object? container, IEnumerable services)
    {
        var parameters = new object[] { services };
        var configurationMethods = type.GetMethods()
                                       .Where(c => c.AnnotatedBy(typeof(ConfigurationMethodAttribute)));

        foreach (var configurationMethod in configurationMethods)
        {
            configurationMethod.Invoke(container, parameters);
        }
    }

    private static void TryInvoke(this Action method)
    {
        try
        {
            method();
        }
        catch
        {
            // ignored
        }
    }
}