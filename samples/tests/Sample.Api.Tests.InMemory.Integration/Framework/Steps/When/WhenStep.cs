using Sample.Api.Tests.InMemory.Integration.Framework.Steps.Base;
using Sample.Api.Tests.InMemory.Integration.Framework.Steps.When.ICall;

namespace Sample.Api.Tests.InMemory.Integration.Framework.Steps.When;

public class WhenStep : RootStep<WhenStep>
{
    public ICallStep ICall => StepFactory<ICallStep>();
}
