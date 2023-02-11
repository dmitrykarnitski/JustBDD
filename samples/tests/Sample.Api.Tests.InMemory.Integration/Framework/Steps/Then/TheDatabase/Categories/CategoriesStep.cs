using FluentAssertions;
using JustBDD.Core;
using Sample.Api.Domain.Categories;
using Sample.Api.Tests.InMemory.Integration.Framework.Steps.Base;

namespace Sample.Api.Tests.InMemory.Integration.Framework.Steps.Then.TheDatabase.Categories;

public class CategoriesStep : Step<ThenStep>
{
    public IAnd<ThenStep> EqualTo(Category category)
    {
        Scenario.Mocks.CategoriesRepository.GetItems().Should().ContainEquivalentOf(category, o => o.RespectingRuntimeTypes());
        return this;
    }
}
