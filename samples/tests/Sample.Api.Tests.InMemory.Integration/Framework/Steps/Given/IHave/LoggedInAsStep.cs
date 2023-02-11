using System;
using JustBDD.Core;
using Sample.Api.Tests.InMemory.Integration.Framework.Authentication;
using Sample.Api.Tests.InMemory.Integration.Framework.Steps.Base;

namespace Sample.Api.Tests.InMemory.Integration.Framework.Steps.Given.IHave;

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
