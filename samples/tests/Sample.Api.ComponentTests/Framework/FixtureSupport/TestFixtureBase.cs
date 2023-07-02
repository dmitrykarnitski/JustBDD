using JustBDD.NUnit;
using JustBDD.NUnit.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Sample.Api.ComponentTests.Framework.BddContexts;
using Sample.Api.ComponentTests.Framework.Logging;
using Sample.Api.ComponentTests.Framework.Steps.Given;
using Sample.Api.ComponentTests.Framework.Steps.Then;
using Sample.Api.ComponentTests.Framework.Steps.When;

namespace Sample.Api.ComponentTests.Framework.FixtureSupport;

[JustBddSettings(typeof(SampleApiJustBddNUnitSettings))]
public class TestFixtureBase : BddFixtureBase<GivenStep, WhenStep, ThenStep>
{
    internal Suite Suite { get; private set; } = null!;

    internal Feature Feature { get; private set; } = null!;

    internal Scenario Scenario { get; private set; } = null!;

    [SetUp]
    public void BaseBeforeEachTest()
    {
        Suite = Suite.Instance;
        Feature = FeatureFactory<Feature>();
        Scenario = ScenarioFactory<Scenario>();

        var scenarioApplication = Suite.Application
                .WithWebHostBuilder(
                    builder => builder
                        .ConfigureLogging(o => o.InterceptApplicationLogs())
                        .ConfigureServices(services => services.AddScenario(Scenario)));

        Scenario.SutHttpClient = scenarioApplication.CreateClient();

        TestContext.Out.WriteLine("Application logs:");
    }

    [TearDown]
    public void BaseAfterEachOfTheTests()
    {
        Scenario.ApplicationLogs.WriteTo(TestContext.Out);
    }
}
