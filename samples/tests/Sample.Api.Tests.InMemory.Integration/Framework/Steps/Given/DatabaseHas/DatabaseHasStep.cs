using System.Collections.Generic;
using JustBDD.Core;
using Sample.Api.Domain.Categories;
using Sample.Api.Tests.InMemory.Integration.Framework.Steps.Base;

namespace Sample.Api.Tests.InMemory.Integration.Framework.Steps.Given.DatabaseHas;

public class DatabaseHasStep : Step<GivenStep>
{
    public IAnd<GivenStep> Category(Category category)
    {
        return Categories(new[] { category });
    }

    public IAnd<GivenStep> Categories(IEnumerable<Category> categories)
    {
        Scenario.Mocks.CategoriesRepository.Initialize(categories);

        return this;
    }
}
