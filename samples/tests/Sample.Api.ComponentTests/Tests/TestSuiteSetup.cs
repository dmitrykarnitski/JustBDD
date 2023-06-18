using System;
using System.Threading.Tasks;
using JustBDD.NUnit.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using Sample.Api.Api.Configuration;
using Sample.Api.Api.RequestPipeline.Authentication;
using Sample.Api.ComponentTests.Framework.Authentication;
using Sample.Api.ComponentTests.Framework.BddContexts;
using Sample.Api.Domain.Categories;

#pragma warning disable CA1052 // NUnit requires setup and tear down to be static methods, but class should be non-static
#pragma warning disable CA2000 // disposing of objects will be done as part of tear down

[assembly: FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[assembly: Parallelizable(ParallelScope.Children)]

namespace Sample.Api.ComponentTests.Tests;

[SetUpFixture]
public class TestSuiteSetup
{
    [OneTimeSetUp]
    public static void InitializeTests()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment(Environments.Production);
                builder.ConfigureTestServices(services =>
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

        application.ClientOptions.BaseAddress = new Uri("https://localhost");

        Suite.Initialize(application);
    }

    [OneTimeTearDown]
    public static async Task CleanUpAllTests()
    {
        await Suite.CleanUpAsync();
    }
}
