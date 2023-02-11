using Sample.Api.Tests.InMemory.Integration.Framework.Steps.Base;
using Sample.Api.Tests.InMemory.Integration.Framework.Steps.Then.TheCall;
using Sample.Api.Tests.InMemory.Integration.Framework.Steps.Then.TheDatabase;

namespace Sample.Api.Tests.InMemory.Integration.Framework.Steps.Then;

public class ThenStep : RootStep<ThenStep>
{
    public TheCallStep TheCall => StepFactory<TheCallStep>();

    public TheDatabaseWillContainStep TheDatabaseWillHave => StepFactory<TheDatabaseWillContainStep>();
}
