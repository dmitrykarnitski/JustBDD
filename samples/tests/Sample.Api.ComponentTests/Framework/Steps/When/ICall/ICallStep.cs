using Sample.Api.ComponentTests.Framework.Steps.Base;
using Sample.Api.ComponentTests.Framework.Steps.When.ICall.TheCategories;

namespace Sample.Api.ComponentTests.Framework.Steps.When.ICall;

public class ICallStep : Step<WhenStep>
{
    public TheCategoriesApiStep TheCategoriesApi => StepFactory<TheCategoriesApiStep>();
}
