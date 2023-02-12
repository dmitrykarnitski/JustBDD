﻿using System.Net.Http;
using JustBDD.Core.Contexts;
using Sample.Api.Tests.InMemory.Integration.Framework.Api;
using Sample.Api.Tests.InMemory.Integration.Framework.BddContexts.ScenarioHelpers;

namespace Sample.Api.Tests.InMemory.Integration.Framework.BddContexts;

/// <summary>
/// Holds shared data that is scoped to the currently running test.
/// </summary>
/// <remarks>
/// <see cref="Suite"/>, <see cref="Feature"/> and <see cref="Scenario"/> classes are defined as internal as they may hold references to internal classes from the SUT.
/// </remarks>
internal class Scenario : ScenarioBase
{
    public ApiClient ApiClient { get; }

    public Mocks Mocks { get; }

    public Scenario()
    {
        ApiClient = new ApiClient(this);
        Mocks = new Mocks(this);
    }

    public string? BearerToken
    {
        get => ContextStore.Get<string>(nameof(BearerToken));
        set => ContextStore.Set(nameof(BearerToken), value);
    }

    public HttpClient SutHttpClient
    {
        get => ContextStore.Get<HttpClient>(nameof(SutHttpClient));
        set => ContextStore.Set(nameof(SutHttpClient), value);
    }

    public HttpResponseMessage HttpResponse
    {
        get => ContextStore.Get<HttpResponseMessage>(nameof(HttpResponse));
        set => ContextStore.Set(nameof(HttpResponse), value);
    }

    public string HttpResponseBody
    {
        get => ContextStore.Get<string>(nameof(HttpResponseBody));
        set => ContextStore.Set(nameof(HttpResponseBody), value);
    }
}