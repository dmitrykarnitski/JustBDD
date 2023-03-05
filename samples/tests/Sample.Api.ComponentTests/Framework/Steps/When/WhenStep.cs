using Sample.Api.ComponentTests.Framework.Steps.Base;
using Sample.Api.ComponentTests.Framework.Steps.When.ICall;

namespace Sample.Api.ComponentTests.Framework.Steps.When;

public class WhenStep : RootStep<WhenStep>
{
    public ICallStep ICall => StepFactory<ICallStep>();
}
