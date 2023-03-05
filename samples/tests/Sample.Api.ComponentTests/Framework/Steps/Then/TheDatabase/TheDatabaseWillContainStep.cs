using Sample.Api.ComponentTests.Framework.Steps.Base;
using Sample.Api.ComponentTests.Framework.Steps.Then.TheDatabase.Categories;

namespace Sample.Api.ComponentTests.Framework.Steps.Then.TheDatabase;

public class TheDatabaseWillContainStep : Step<ThenStep>
{
    public CategoriesStep Categories => StepFactory<CategoriesStep>();
}
