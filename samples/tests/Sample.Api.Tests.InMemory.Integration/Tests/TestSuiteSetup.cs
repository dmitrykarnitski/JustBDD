﻿using System;
using System.Diagnostics;
using JustBDD.NUnit.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Sample.Api.Api.Configuration;
using Sample.Api.Api.RequestPipeline.Authentication;
using Sample.Api.Domain.Categories;
using Sample.Api.Tests.InMemory.Integration.Framework.Authentication;
using Sample.Api.Tests.InMemory.Integration.Framework.BddContexts;
using Sample.Api.Tests.InMemory.Integration.Framework.Logging;

#pragma warning disable CA1052 // NUnit requires setup and tear down to be static methods, but class should be non-static
#pragma warning disable CA2000 // disposing of objects will be done as part of tear down

[assembly: FixtureLifeCycle(LifeCycle.InstancePerTestCase)]

namespace Sample.Api.Tests.InMemory.Integration.Tests;

[SetUpFixture]
public class TestSuiteSetup
{
    [OneTimeSetUp]
    public static void InitializeTests()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureLogging(logging => { logging.ClearProviders(); });
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(_ =>
                        new AuthenticationConfiguration
                        {
                            ClientId = Guid.NewGuid().ToString(),
                            ClientSecret = Guid.NewGuid().ToString(),
                            Issuer = MockAuthenticationConstants.Issuer,
                            Resource = MockAuthenticationConstants.Resource
                        });
                    services.AddSingleton<IJwtBearerOptionsConfigurationApplier, MockedJwtBearerOptionsConfigurationApplier>();

                    services.UseDependencyResolutionFromScenario<Scenario>()
                        .Add<ICategoriesRepository>(scenario => scenario.Mocks.CategoriesRepository);
                });
            });

        var loggerFactory = application.Services.GetRequiredService<ILoggerFactory>();
        loggerFactory.AddProvider(new EnhancedDebugLoggerProvider());

        Trace.Listeners.Add(new TestOutputTraceListener());

        Suite.Initialize(application);
    }

    [OneTimeTearDown]
    public static void CleanUpAllTests()
    {
        Suite.CleanUp();
    }
}