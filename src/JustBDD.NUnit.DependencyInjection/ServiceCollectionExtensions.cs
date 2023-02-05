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
}
