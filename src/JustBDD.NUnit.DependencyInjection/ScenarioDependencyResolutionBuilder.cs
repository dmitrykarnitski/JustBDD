using System;
using JustBDD.Core.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace JustBDD.NUnit.DependencyInjection;

public class ScenarioDependencyResolutionBuilder<TScenario>
    where TScenario : ScenarioBase, new()
{
    private readonly IServiceCollection _services;

    public ScenarioDependencyResolutionBuilder(IServiceCollection services)
    {
        _services = services;
    }

    public ScenarioDependencyResolutionBuilder<TScenario> Add<TAbstraction>(Func<TScenario, TAbstraction> resolver)
        where TAbstraction : class
    {
        _ = _services.AddTransient(sp => resolver(ResolveScenario(sp)));

        return this;
    }

    public ScenarioDependencyResolutionBuilder<TScenario> Add<TAbstraction>(Func<TScenario, IServiceProvider, TAbstraction> resolver)
        where TAbstraction : class
    {
        _ = _services.AddTransient(sp => resolver(ResolveScenario(sp), sp));

        return this;
    }

    private TScenario ResolveScenario(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredService<TScenario>();
    }
}
