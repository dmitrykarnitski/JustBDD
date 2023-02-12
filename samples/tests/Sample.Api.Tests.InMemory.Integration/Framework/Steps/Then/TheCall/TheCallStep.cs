﻿using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using JustBDD.Core;
using Newtonsoft.Json;
using Sample.Api.Tests.InMemory.Integration.Framework.Steps.Base;

namespace Sample.Api.Tests.InMemory.Integration.Framework.Steps.Then.TheCall;

public class TheCallStep : Step<ThenStep>
{
    public IAnd<ThenStep> WillSucceed()
    {
        WillSucceedWithStatusCode(HttpStatusCode.OK);

        return this;
    }

    public IAnd<ThenStep> WillSucceedWithStatusCode(HttpStatusCode statusCode)
    {
        Scenario.HttpResponse.StatusCode.Should().Be(statusCode);

        return this;
    }

    public IAnd<ThenStep> WillFailWithAuthenticationError()
    {
        Scenario.HttpResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        return this;
    }

    public IAnd<ThenStep> WillHaveAResponseEqualTo<TResponse>(TResponse expectedResponse)
    {
        Scenario.HttpResponseBody.Should().NotBeEmpty();

        var actualResponse = JsonConvert.DeserializeObject<TResponse>(Scenario.HttpResponseBody);

        actualResponse.Should().BeEquivalentTo(expectedResponse, o => o.RespectingRuntimeTypes());

        return this;
    }

    public IAnd<ThenStep> WillHaveAResponseEqualTo<TItem>(IEnumerable<TItem> expectedResponse, bool checkOrdering = false)
    {
        Scenario.HttpResponseBody.Should().NotBeEmpty();

        var actualResponse = JsonConvert.DeserializeObject<IEnumerable<TItem>>(Scenario.HttpResponseBody);

        actualResponse.Should().AllBeEquivalentTo(
            expectedResponse,
            o =>
            {
                if (checkOrdering)
                {
                    o.WithStrictOrdering();
                }

                o.RespectingRuntimeTypes();

                return o;
            });

        return this;
    }
}