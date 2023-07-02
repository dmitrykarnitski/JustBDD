using System.Net;
using FluentAssertions;
using JustBDD.Core;
using Sample.Api.ComponentTests.Framework.Steps.Base;
using Sample.Api.ComponentTests.Framework.Steps.Then.TheCall.WillFailWith;
using Sample.Api.ComponentTests.Framework.Steps.Then.TheCall.WillHaveAResponse;

namespace Sample.Api.ComponentTests.Framework.Steps.Then.TheCall;

public class TheCallStep : Step<ThenStep>
{
    public WillFailWithStep WillFailWith => StepFactory<WillFailWithStep>();

    public WillHaveAResponseStep WillHaveAResponse => StepFactory<WillHaveAResponseStep>();

    public IAnd<ThenStep> WillSucceed()
    {
        WillSucceedWithStatusCode(HttpStatusCode.OK);

        return this;
    }

    public IAnd<ThenStep> WillSucceedWithStatusCode(HttpStatusCode statusCode)
    {
        Scenario.HttpResponse.Should().NotBeNull();

        Scenario.HttpResponse!.StatusCode.Should().Be(statusCode);

        return this;
    }
}
