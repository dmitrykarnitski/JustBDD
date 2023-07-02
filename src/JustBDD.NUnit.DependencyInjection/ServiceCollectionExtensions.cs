using JustBDD.Core.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace JustBDD.NUnit.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static ScenarioDependencyResolutionBuilder<TScenario> UseDependencyResolutionFromScenario<TScenario>(this IServiceCollection services)
        where TScenario : ScenarioBase, new()
    {
        return new ScenarioDependencyResolutionBuilder<TScenario>(services);
    }

    public static IServiceCollection AddScenario<TScenario>(this IServiceCollection services, TScenario scenario)
        where TScenario : ScenarioBase, new()
    {
        services.AddSingleton(_ => scenario);

        return services;
    }
}
