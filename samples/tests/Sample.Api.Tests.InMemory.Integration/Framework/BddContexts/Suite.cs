using System;
using System.Threading.Tasks;
using AutoMapper;
using JustBDD.Core.Contexts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Api.Tests.InMemory.Integration.Framework.BddContexts;

/// <summary>
/// Holds shared data that is global the whole testing suite.
/// </summary>
/// <remarks>
/// <see cref="Suite"/>, <see cref="Feature"/> and <see cref="Scenario"/> classes are defined as internal as they may hold references to internal classes from the SUT.
/// </remarks>
internal class Suite : SuiteBase
{
    public static readonly Suite Instance = new();

    private Suite()
    {
    }

    public WebApplicationFactory<Program> Application => ContextStore.Get<WebApplicationFactory<Program>>(nameof(Application));

    public IServiceProvider ServiceProvider => ContextStore.Get<IServiceProvider>(nameof(ServiceProvider));

    public IMapper Mapper => GetAndResolveIfNotSet<IMapper>(nameof(Mapper));

    public static void Initialize(WebApplicationFactory<Program> applicationFactory)
    {
        Instance.ContextStore.Set(nameof(Application), applicationFactory);
        Instance.ContextStore.Set(nameof(ServiceProvider), applicationFactory.Services);
    }

    public static async Task CleanUpAsync()
    {
        await Instance.DisposeAsync();
    }

    private T GetAndResolveIfNotSet<T>(string propertyName)
        where T : notnull
    {
        if (!ContextStore.Contains(propertyName))
        {
            ContextStore.Set(propertyName, ServiceProvider.GetRequiredService<T>());
        }

        return ContextStore.Get<T>(propertyName);
    }
}
