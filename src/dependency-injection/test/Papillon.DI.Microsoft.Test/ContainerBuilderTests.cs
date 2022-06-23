using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Papillon.DI.Microsoft;

public class ContainerBuilderTests
{
    [Fact]
    public void ConfigureServices()
    {
        var serviceProvider = BuildServiceProvider();
        var service = serviceProvider.GetService<ICustomService>();

        Assert.NotNull(service);
    }

    private static IServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();
        services.AddPapillon();

        return services.BuildServiceProvider();
    }
}

public interface ICustomService
{
}

public class CustomService : ICustomService
{
}

[ContainerBuilder]
public static class ModuleBuilder
{
    [ConfigurationMethod]
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddTransient<ICustomService, CustomService>();
    }
}