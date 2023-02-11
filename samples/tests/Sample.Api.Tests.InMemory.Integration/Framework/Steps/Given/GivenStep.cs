using Sample.Api.Tests.InMemory.Integration.Framework.Steps.Base;
using Sample.Api.Tests.InMemory.Integration.Framework.Steps.Given.DatabaseHas;
using Sample.Api.Tests.InMemory.Integration.Framework.Steps.Given.IHave;

namespace Sample.Api.Tests.InMemory.Integration.Framework.Steps.Given;

public class GivenStep : RootStep<GivenStep>
{
    public IHaveStep IHave => StepFactory<IHaveStep>();

    public DatabaseHasStep DatabaseHas => StepFactory<DatabaseHasStep>();
}
