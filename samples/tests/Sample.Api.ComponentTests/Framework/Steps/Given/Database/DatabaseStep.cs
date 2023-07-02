using System.Collections.Generic;
using JustBDD.Core;
using Sample.Api.ComponentTests.Framework.Steps.Base;
using Sample.Api.Domain.Categories;

namespace Sample.Api.ComponentTests.Framework.Steps.Given.Database;

public class DatabaseStep : Step<GivenStep>
{
    public IAnd<GivenStep> HasCategory(Category category)
    {
        return HaveCategories(new[] { category });
    }

    public IAnd<GivenStep> HaveCategories(IEnumerable<Category> categories)
    {
        Scenario.Mocks.CategoriesRepository.Initialize(categories);

        return this;
    }

    public IAnd<GivenStep> IsUnavailable()
    {
        Scenario.Mocks.CategoriesRepository.WillThrowAnException();

        return this;
    }
}
