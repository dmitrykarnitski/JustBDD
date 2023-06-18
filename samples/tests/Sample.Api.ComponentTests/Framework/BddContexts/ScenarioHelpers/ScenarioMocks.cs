using Sample.Api.ComponentTests.TestHelpers.Mocks.Categories;

namespace Sample.Api.ComponentTests.Framework.BddContexts.ScenarioHelpers;

public class ScenarioMocks
{
    public CategoriesRepositoryMock CategoriesRepository { get; } = new();
}
