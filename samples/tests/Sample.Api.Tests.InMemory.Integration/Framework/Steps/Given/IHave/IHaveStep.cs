using JustBDD.Core;
using Sample.Api.Tests.InMemory.Integration.Framework.Authentication;
using Sample.Api.Tests.InMemory.Integration.Framework.Steps.Base;

namespace Sample.Api.Tests.InMemory.Integration.Framework.Steps.Given.IHave;

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
