using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Papillon.DI.Microsoft.Reflection;
using Scrutor;

namespace Papillon.DI.Microsoft;

public static class DependencyRegistrar
{
    public static void AddPapillon(this IServiceCollection services)
    {
        var assemblies = AssemblyHelper.LoadReferencedAssemblies();

        services.AddModules(assemblies);
        services.AddPapillon(assemblies);
    }

    public static void AddPapillon(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPapillon();
        services.ConfigurePapillon(configuration);
    }

    private static void AddPapillon(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.ScanAndRegister(assemblies, InjectionMode.Singleton);
        services.ScanAndRegister(assemblies, InjectionMode.Scoped);
        services.ScanAndRegister(assemblies, InjectionMode.Transient);
    }

    private static void ScanAndRegister(this IServiceCollection services,
                                        Assembly[] assemblies,
                                        InjectionMode injectionMode)
    {
        if (assemblies.Length == 0)
        {
            return;
        }

        services.Scan(scan =>
        {
            var typeSelector = scan.FromAssemblies(assemblies)
                                   .AddClasses(classes => classes.Where(type => type.ShouldInject(injectionMode)));

            typeSelector.AsImplementedInterfaces()
                        .Register(injectionMode);
        });
    }

    private static void Register(this ILifetimeSelector lifeTimeSelector, InjectionMode injectionMode)
    {
        switch (injectionMode)
        {
            case InjectionMode.Singleton:
                lifeTimeSelector.WithSingletonLifetime();

                break;
            case InjectionMode.Scoped:
                lifeTimeSelector.WithScopedLifetime();

                break;
            case InjectionMode.Transient:
                lifeTimeSelector.WithTransientLifetime();

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(injectionMode), injectionMode, null);
        }
    }

    private static bool ShouldInject(this Type type, InjectionMode injectionMode)
    {
        if (type.GetInjectableAttribute() is not InjectableAttribute injectableAttribute)
        {
            return false;
        }

        return injectableAttribute.Mode == injectionMode;
    }

    private static Attribute? GetInjectableAttribute(this Type type)
    {
        var injectableAttribute = type.GetCustomAttributes()
                                      .FirstOrDefault(attribute => attribute.IsInjectable());

        if (injectableAttribute is not null)
        {
            return injectableAttribute;
        }

        return type.GetInterfaces()
                   .SelectMany(@interface => @interface.GetCustomAttributes())
                   .FirstOrDefault(attribute => attribute.IsInjectable());
    }

    private static bool IsInjectable(this Attribute attribute)
    {
        var injectionAttributeType = typeof(InjectableAttribute);

        return attribute.GetType()
                        .IsSubclassOf(injectionAttributeType);
    }
}