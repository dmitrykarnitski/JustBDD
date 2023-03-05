using JustBDD.Core;
using Sample.Api.ComponentTests.Framework.Authentication;
using Sample.Api.ComponentTests.Framework.Steps.Base;

namespace Sample.Api.ComponentTests.Framework.Steps.Given.IHave;

public class IHaveStep : Step<GivenStep>
{
    public LoggedInAsStep LoggedInAs => StepFactory<LoggedInAsStep>();

    public IAnd<GivenStep> AnEmptyBearerToken()
    {
        Scenario.BearerToken = string.Empty;

        return this;
    }

    public IAnd<GivenStep> AnExpiredBearerToken()
    {
        Scenario.BearerToken = new JwtTokenBuilder()
            .WithValidMockedAuthConfiguration()
            .WithAnExpiresAtInThePast()
            .Build();

        return this;
    }
}
