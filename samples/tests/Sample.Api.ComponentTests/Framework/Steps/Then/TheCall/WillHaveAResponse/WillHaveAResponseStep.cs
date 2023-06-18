using System.Collections.Generic;
using System.Text.Json;
using FluentAssertions;
using JustBDD.Core;
using Sample.Api.ComponentTests.Framework.Steps.Base;

namespace Sample.Api.ComponentTests.Framework.Steps.Then.TheCall.WillHaveAResponse;

public class WillHaveAResponseStep : Step<ThenStep>
{
    private static readonly JsonSerializerOptions defaultSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public IAnd<ThenStep> EqualTo<TResponse>(TResponse expectedResponse)
    {
        Scenario.HttpResponseBody.Should().NotBeEmpty();

        var actualResponse = JsonSerializer.Deserialize<TResponse>(Scenario.HttpResponseBody!, defaultSerializerOptions);

        actualResponse.Should().BeEquivalentTo(expectedResponse, o => o.RespectingRuntimeTypes());

        return this;
    }

    public IAnd<ThenStep> CollectionEqualTo<TItem>(IEnumerable<TItem> expectedResponse, bool checkOrdering = false)
    {
        Scenario.HttpResponseBody.Should().NotBeEmpty();

        var actualResponse = JsonSerializer.Deserialize<IEnumerable<TItem>>(Scenario.HttpResponseBody!, defaultSerializerOptions);

        actualResponse.Should().BeEquivalentTo(
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
