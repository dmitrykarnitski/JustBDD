using Sample.Api.ComponentTests.Framework.Steps.Base;
using Sample.Api.ComponentTests.Framework.Steps.Then.TheDatabaseWillContain.Categories;

namespace Sample.Api.ComponentTests.Framework.Steps.Then.TheDatabaseWillContain;

public class TheDatabaseWillContainStep : Step<ThenStep>
{
    public CategoriesStep Categories => StepFactory<CategoriesStep>();
}
