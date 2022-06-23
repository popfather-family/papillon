using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Papillon.DI.Microsoft;

internal static class OptionsRegistrar
{
    internal static void ConfigurePapillon(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        services.ConfigurePapillon(configuration, assemblies);
    }

    private static void ConfigurePapillon(this IServiceCollection services,
                                          IConfiguration configuration,
                                          params Assembly[] assemblies)
    {
        var options = assemblies.SelectMany(assembly => assembly.FindOptions());
        foreach (var option in options)
        {
            services.ConfigureOption(configuration, option);
        }
    }

    private static IEnumerable<(Type Type, string Section)> FindOptions(this Assembly assembly)
    {
        return from type in assembly.GetTypes()
               let optionsAttribute = (OptionsAttribute?)type.GetCustomAttributes()
                                                             .FirstOrDefault(c => c.GetType() == typeof(OptionsAttribute))
               where optionsAttribute is not null
               select (type, optionsAttribute.Section);
    }

    private static void ConfigureOption(this IServiceCollection services,
                                        IConfiguration configuration,
                                        (Type, string) option)
    {
        (var type, string sectionValue) = option;
        var section = configuration.GetSection(sectionValue);
        var extensionType = typeof(OptionsConfigurationServiceCollectionExtensions);
        const string MethodName = nameof(OptionsConfigurationServiceCollectionExtensions.Configure);

        extensionType.GetMethods()
                     .Where(methodInfo => methodInfo.Name == MethodName)
                     .First(methodInfo =>
                     {
                         var parameters = methodInfo.GetParameters()
                                                    .ToArray();

                         return parameters.Length == 2 &&
                                parameters[0]
                                    .ParameterType ==
                                typeof(IServiceCollection) &&
                                parameters[1]
                                    .ParameterType ==
                                typeof(IConfiguration);
                     })
                     .MakeGenericMethod(type)
                     .Invoke(null, new object[] { services, section });
    }
}