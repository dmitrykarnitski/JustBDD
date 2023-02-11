using Sample.Api.Tests.InMemory.Integration.Framework.Steps.Base;
using Sample.Api.Tests.InMemory.Integration.Framework.Steps.Then.TheDatabase.Categories;

namespace Sample.Api.Tests.InMemory.Integration.Framework.Steps.Then.TheDatabase;

public class TheDatabaseWillContainStep : Step<ThenStep>
{
    public CategoriesStep Categories => StepFactory<CategoriesStep>();
}
