using Sample.Api.Tests.InMemory.Integration.TestHelpers.Mocks.Categories;

namespace Sample.Api.Tests.InMemory.Integration.Framework.BddContexts.ScenarioHelpers;

internal class Mocks
{
    public MockedCategoriesRepository CategoriesRepository { get; } = new();
}
