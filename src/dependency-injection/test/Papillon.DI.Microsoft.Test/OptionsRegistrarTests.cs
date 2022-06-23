using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Papillon.DI.Microsoft.Fakes;
using Xunit;

namespace Papillon.DI.Microsoft;

public class OptionsRegistrarTests
{
    [Fact]
    public void Options_Should_Resolve()
    {
        var serviceProvider = BuildServiceProvider();
        var options = serviceProvider.GetService<IOptions<FakeOptions>>();

        Assert.NotNull(options);
    }

    [Fact]
    public void Validate_Options_Values()
    {
        var serviceProvider = BuildServiceProvider();
        var options = serviceProvider.GetService<IOptions<FakeOptions>>();

        Assert.Equal(123, options?.Value.Id);
        Assert.Equal("secret", options?.Value.Secret);
    }

    private static IServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();

        var configuration = new ConfigurationBuilder().AddJsonFile("appSettings.json")
                                                      .Build();

        services.AddPapillon(configuration);

        return services.BuildServiceProvider();
    }
}