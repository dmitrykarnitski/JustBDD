using JustBDD.Core.Contexts.Stores;
using Sample.Api.Tests.InMemory.Integration.TestHelpers.Mocks.Categories;

namespace Sample.Api.Tests.InMemory.Integration.Framework.BddContexts.ScenarioHelpers;

internal class Mocks
{
    private readonly Scenario _scenario;

    public Mocks(Scenario scenario)
    {
        _scenario = scenario;
    }

    public MockedCategoriesRepository CategoriesRepository
    {
        get => _scenario.ContextStore.GetAndInitialiseIfNotSet<MockedCategoriesRepository>(nameof(CategoriesRepository));
        set => _scenario.ContextStore.Set(nameof(CategoriesRepository), value);
    }
}
