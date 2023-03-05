using FluentAssertions;
using JustBDD.Core;
using Sample.Api.ComponentTests.Framework.Steps.Base;
using Sample.Api.Domain.Categories;

namespace Sample.Api.ComponentTests.Framework.Steps.Then.TheDatabase.Categories;

public class CategoriesStep : Step<ThenStep>
{
    public IAnd<ThenStep> EqualTo(Category category)
    {
        Scenario.Mocks.CategoriesRepository.GetItems().Should().ContainEquivalentOf(category, o => o.RespectingRuntimeTypes());
        return this;
    }
}
