using Sample.Api.ComponentTests.TestHelpers.Mocks.Categories;

namespace Sample.Api.ComponentTests.Framework.BddContexts.ScenarioHelpers;

internal class Mocks
{
    public MockedCategoriesRepository CategoriesRepository { get; } = new();
}
