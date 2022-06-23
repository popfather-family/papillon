using System;
using System.Collections.Generic;
using System.Linq;
using Fakes.DI.LazyLoadAssembly.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Papillon.DI.Microsoft.Fakes;
using Xunit;

namespace Papillon.DI.Microsoft;

public class DependencyRegistrarTests
{
    [Fact]
    public void Scoped_Service_Should_Resolve()
    {
        var serviceProvider = BuildServiceProvider();

        var service = serviceProvider.GetService<IScopedService>();
        var serviceType = service?.GetType();

        Assert.Equal(typeof(ScopedService), serviceType);
    }

    [Fact]
    public void Singleton_Service_Should_Resolve()
    {
        var serviceProvider = BuildServiceProvider();

        var service = serviceProvider.GetService<ISingletonService>();
        var serviceType = service?.GetType();

        Assert.Equal(typeof(SingletonService), serviceType);
    }

    [Fact]
    public void Singleton_Service_Should_Inject_By_Singleton_Lifetime()
    {
        var serviceProvider = BuildServiceProvider();

        var service1 = serviceProvider.GetService<ISingletonService>();
        var service2 = serviceProvider.GetService<ISingletonService>();

        Assert.Equal(service1, service2);
    }

    [Fact]
    public void Scoped_Service_Should_Inject_By_Scoped_Lifetime()
    {
        var serviceProvider = BuildServiceProvider();

        var scope1 = serviceProvider.CreateScope();
        var service1 = scope1.ServiceProvider.GetService<IScopedService>();
        var service2 = scope1.ServiceProvider.GetService<IScopedService>();

        Assert.Equal(service1, service2);

        var scope2 = serviceProvider.CreateScope();
        var service3 = scope2.ServiceProvider.GetService<IScopedService>();

        Assert.NotEqual(service3, service2);
        Assert.NotEqual(service3, service1);
    }

    [Fact]
    public void Should_Scan_Services_From_Unloaded_Assemblies()
    {
        var serviceProvider = BuildServiceProvider();
        var service = serviceProvider.GetService<ILazyLoadService>();

        Assert.NotNull(service);
    }

    [Fact]
    public void Should_Resolve_Extensible_Types()
    {
        var serviceProvider = BuildServiceProvider();
        var behaviors = serviceProvider.GetService<IEnumerable<IBehavior>>();

        Assert.Equal(2, behaviors?.Count());
    }

    [Fact]
    public void Should_Resolve_Inherited_Extensible_Types()
    {
        var serviceProvider = BuildServiceProvider();
        var behavior = serviceProvider.GetService<IExtendedBehavior>();

        Assert.NotNull(behavior);
    }

    [Fact]
    public void Extensible_Service_Should_Inject_By_Overridden_Lifetime()
    {
        var serviceProvider = BuildServiceProvider();

        var scope1 = serviceProvider.CreateScope();
        var service1 = scope1.ServiceProvider.GetService<IScopedBehavior>();
        var service2 = scope1.ServiceProvider.GetService<IScopedBehavior>();

        Assert.Equal(service1, service2);

        var scope2 = serviceProvider.CreateScope();
        var service3 = scope2.ServiceProvider.GetService<IScopedBehavior>();

        Assert.NotEqual(service3, service2);
        Assert.NotEqual(service3, service1);
    }

    [Fact]
    public void Should_Resolve_Generic_Types()
    {
        var serviceProvider = BuildServiceProvider();

        var pingService = serviceProvider.GetService<IScopedService<Ping>>();
        Assert.NotNull(pingService);

        var pongService = serviceProvider.GetService<IScopedService<Pong>>();
        Assert.NotNull(pongService);

        var behaviorService = serviceProvider.GetService<IExtendedBehavior<Behavior>>();
        Assert.NotNull(behaviorService);
    }

    [Fact]
    public void Should_Resolve_Inherited_Generic_Extensible_Types()
    {
        var serviceProvider = BuildServiceProvider();
        var behaviors = serviceProvider.GetService<IEnumerable<IBehavior<Behavior>>>();

        Assert.Equal(2, behaviors?.Count());
    }

    private static IServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();
        services.AddPapillon();

        return services.BuildServiceProvider();
    }
}