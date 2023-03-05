using System;
using JustBDD.Core;
using Sample.Api.ComponentTests.Framework.Authentication;
using Sample.Api.ComponentTests.Framework.Steps.Base;

namespace Sample.Api.ComponentTests.Framework.Steps.Given.IHave;

public class LoggedInAsStep : Step<GivenStep>
{
    public IAnd<GivenStep> AValidUser()
    {
        Scenario.BearerToken = new JwtTokenBuilder()
            .WithValidMockedAuthConfiguration()
            .WithSubject(Guid.NewGuid().ToString())
            .Build();

        return this;
    }
}
