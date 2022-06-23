# Papillon DI

It is a simple and distributed solution to add dependencies based on attributes. Papillon DI is an abstraction that can implement by DI engines like Microsoft, AutoFac, ...

We have implemented Papillon DI contracts by Microsoft dependency injection by default. You can implement it by other packages and request to us to put it in Papillon DI codebase or publish your package as Papillon extension.

⭐️ We appreciate your star, it helps!

## Getting Started

Get the latest stable release from the [nuget.org](http://www.nuget.org/packages/papillon-di)

In your Startup.cs, you should add Papillon by following codes.

```C#
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Papillon.DI.Microsoft;

namespace Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPapillon(Configuration); // register services and options
            // services.AddPapillon(); // register services only
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // configure
        }
    }
}
```

## Service Injection

### Singleton
```C#
public interface ICacheService
{
    Task PutAsync<TValue>(string key, TValue value);
    
    Task<TValue> GetAsync<TValue>(string key);
}

[Singleton]
public class RedisCacheService : ICacheService
{
    private readonly IDatabase db;

    public RedisCacheService(IDatabase db)
    {
        this.db = db;
    }

    public Task PutAsync<TValue>(string key, TValue value)
    {
        // put item to redis
    }
    
    public Task<TValue> GetAsync<TValue>(string key)
    {
        // get and deserialized as TValue from redis
    }
}
```

### Scoped
```C#
public interface IApplicationContext
{
    Task<User> GetCurrentUserAsync();
}

[Scoped]
public class ApplicationContext : IApplicationContext
{
    public Task<User> GetCurrentUserAsync()
    {
        // return current user
    }
}
```

### Transient
```C#
public interface IIdentityService
{
    Task<AuthenticationResponse> AuthenticateAsync(string username, string password);
}

[Transient]
public class IdentityService : IIdentityService
{
    public Task<AuthenticationResponse> AuthenticateAsync(string username, string password)
    {
        // return authentication response
    }
}
```

For extensible interfaces you apply Open-Closed principle, you can annotate your interface by ExtensibleAttribute, Papillon DI engine automatically find all classes that implemented annotated interface and inject all instances by **Transient** lifetime by default, but you can override it by another lifetime.

```C#
[Extensible]
public interface IDeveloperSalaryCalculator
{
    DeveloperSeniority DeveloperSeniority { get; }

    Money Calculate(Hours workingHours);
}

public class JuniorDeveloperSalaryCalculator : IDeveloperSalaryCalculator
{
    public DeveloperSeniority DeveloperSeniority => DeveloperSeniority.Junior;

    public Money Calculate(Hours workingHours)
    {
        return Money.Euro(workingHours * 15);
    }
}

public class SeniorDeveloperSalaryCalculator : IDeveloperSalaryCalculator
{
    public DeveloperSeniority DeveloperSeniority => DeveloperSeniority.Senior;

    public Money Calculate(Hours workingHours)
    {
        return Money.Euro(workingHours * 30);
    }
}

public interface ISalaryCalculator
{
    Task CalculateAsync(IEnumerable<Developer> developers);
}

[Trainsient]
public interface SalaryCalculator : ISalaryCalculator
{
    private readonly Dictionary<DeveloperSeniority, IDeveloperSalaryCalculator> salaryCalculators;
    private readonly ISalaryRepository salaryRepository;

    public SalaryCalculator(IEnumerable<IDeveloperSalaryCalculator> salaryCalculators,
                            ISalaryRepository salaryRepository)
    {
        this.salaryCalculators = salaryCalculators.ToDictionary(c => c.DeveloperSeniority);
        this.salaryRepository = salaryRepository;
    }
    
    public async Task CalculateAsync(IEnumerable<DeveloperWorkingInfo> workingInformations)
    {
        foreach(var workingInformation in workingInformations)
        {
            var calculator = salaryCalculators[workingInformation.Developer.Seniority];
            var salary = calculator.Calculate(workingInformation.WorkingHours);
            
            await salaryRepository.PutAsync(workingInformation.Developer, salary);
        }
    }
}
```

## Option Injection

You should annotate your Option class by OptionsAttribute and map it to its value section from configuration file.

```json
{
  "Redis": {
    "IPAddress": "192.168.1.1",
    "Port": "2525",
    "Username": "Admin",
    "Password": "Admin"
  }
}
```

```C#
[Options("Redis")]
public class RedisOptions
{
    public string IPAddress { get; init; }
    
    public int Port { get; init; }
    
    public string Username { get; init; }
    
    public string Password { get; init; }
}

[Singleton]
public class RedisBuilder : IRedisBuilder
{
    private readonly RedisOptions options;

    public RedisBuilder(IOptions<RedisOptions> options)
    {
        this.options = options.Value;
    }
    
    public void Build() { }
}
```

## Modular monolith architecture

If your application has many modules and aggregate these into an AppDomain, you can use Papillon builder pattern to manage external services in each module builder.

You should create builder class (static or non-static) and annotate it by ContainerBuilderAttribute, then write a method and annotate it by ConfigurationMethodAttribute, if you use Microsoft package you should write a method that get IServiceCollection as input parameter. Papillon engine find your builder method and call it when **AddPapillon** called.

### ContainerBuilder

Imagine Redis have been used to implement our cache module. unfortunately StackExchange package does not compatible with Papillon. so you can create a builder and add StackExchange to Papillon.

```C#
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Papillon.DI;
using StackExchange.Redis;

namespace Papillon.Storage.Builder;

[ContainerBuilder]
public static class StackExchangeBuilder
{
    [ConfigurationMethod]
    public static void AddStackExchange(IServiceCollection services)
    {
        services.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
        {
            var options = serviceProvider.GetService<IOptions<RedisOptions>>();
            var connectionString = $"{options?.Value.IPAddress}:{options?.Value.Port}";

            return ConnectionMultiplexer.Connect(connectionString);
        });

        services.AddSingleton<IDatabase>(serviceProvider =>
        {
            var multiplexer = serviceProvider.GetService<IConnectionMultiplexer>()!;

            return multiplexer.GetDatabase();
        });
    }
}
```
