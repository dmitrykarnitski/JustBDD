using JustBDD.Core.Contexts;
using JustBDD.NUnit.TestProperties;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace JustBDD.NUnit;

public class ScenarioDependencyResolutionBuilder<TScenario> where TScenario : ScenarioBase, new()
{
    private readonly IServiceCollection _services;

    public ScenarioDependencyResolutionBuilder(IServiceCollection services)
    {
        _services = services;
    }

    public ScenarioDependencyResolutionBuilder<TScenario> Add<TAbstraction>(Func<TScenario, TAbstraction> resolver)
        where TAbstraction : class
    {
        _services.AddTransient(_ => resolver(CreateScenarioFromTestContext()));

        return this;
    }

    public ScenarioDependencyResolutionBuilder<TScenario> Add<TAbstraction>(Func<TScenario, IServiceProvider, TAbstraction> resolver)
        where TAbstraction : class
    {
        _services.AddTransient(sp => resolver(CreateScenarioFromTestContext(), sp));

        return this;
    }

    private TScenario CreateScenarioFromTestContext()
    {
        var scnearioStore = TestContextInstance.Current.Test.Properties.GetScenarioStore();
        return ContextFactory.Create<TScenario>(scnearioStore);
    }
}
