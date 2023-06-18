using JustBDD.Core;
using Sample.Api.ComponentTests.Framework.Steps.Base;
using Sample.Api.ComponentTests.Framework.Steps.When.ICall.TheCategoriesApi;

namespace Sample.Api.ComponentTests.Framework.Steps.When.ICall;

public class ICallStep : Step<WhenStep>
{
    public TheCategoriesApiStep TheCategoriesApi => StepFactory<TheCategoriesApiStep>();

    public IAnd<WhenStep> AnyEndpoint()
    {
        TheCategoriesApi.GetAllEndpoint();

        return this;
    }
}
