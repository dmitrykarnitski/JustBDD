using Sample.Api.ComponentTests.Framework.Steps.Base;
using Sample.Api.ComponentTests.Framework.Steps.Given.Database;
using Sample.Api.ComponentTests.Framework.Steps.Given.IHave;

namespace Sample.Api.ComponentTests.Framework.Steps.Given;

public class GivenStep : RootStep<GivenStep>
{
    public IHaveStep IHave => StepFactory<IHaveStep>();

    public DatabaseStep Database => StepFactory<DatabaseStep>();
}
