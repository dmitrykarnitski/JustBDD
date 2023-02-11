using Sample.Api.Tests.InMemory.Integration.Framework.Steps.Base;
using Sample.Api.Tests.InMemory.Integration.Framework.Steps.When.ICall.TheCategories;

namespace Sample.Api.Tests.InMemory.Integration.Framework.Steps.When.ICall;

public class ICallStep : Step<WhenStep>
{
    public TheCategoriesApiStep TheCategoriesApi => StepFactory<TheCategoriesApiStep>();
}
