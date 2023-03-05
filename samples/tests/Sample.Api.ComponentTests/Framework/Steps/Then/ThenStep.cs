using Sample.Api.ComponentTests.Framework.Steps.Base;
using Sample.Api.ComponentTests.Framework.Steps.Then.TheCall;
using Sample.Api.ComponentTests.Framework.Steps.Then.TheDatabase;

namespace Sample.Api.ComponentTests.Framework.Steps.Then;

public class ThenStep : RootStep<ThenStep>
{
    public TheCallStep TheCall => StepFactory<TheCallStep>();

    public TheDatabaseWillContainStep TheDatabaseWillHave => StepFactory<TheDatabaseWillContainStep>();
}
