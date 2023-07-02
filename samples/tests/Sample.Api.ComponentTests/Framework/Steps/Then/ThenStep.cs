using Sample.Api.ComponentTests.Framework.Steps.Base;
using Sample.Api.ComponentTests.Framework.Steps.Then.ApplicationLogs;
using Sample.Api.ComponentTests.Framework.Steps.Then.TheCall;
using Sample.Api.ComponentTests.Framework.Steps.Then.TheDatabaseWillContain;

namespace Sample.Api.ComponentTests.Framework.Steps.Then;

public class ThenStep : RootStep<ThenStep>
{
    public TheCallStep TheCall => StepFactory<TheCallStep>();

    public ApplicationLogsStep ApplicationLogs => StepFactory<ApplicationLogsStep>();

    public TheDatabaseWillContainStep TheDatabaseWillHave => StepFactory<TheDatabaseWillContainStep>();
}
