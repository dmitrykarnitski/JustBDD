using JustBDD.NUnit;
using NUnit.Framework;
using Sample.Api.ComponentTests.Framework.BddContexts;
using Sample.Api.ComponentTests.Framework.Logging;
using Sample.Api.ComponentTests.Framework.Steps.Given;
using Sample.Api.ComponentTests.Framework.Steps.Then;
using Sample.Api.ComponentTests.Framework.Steps.When;

namespace Sample.Api.ComponentTests.Framework.FixtureSupport;

[TestFixture]
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

        Scenario.SutHttpClient = Suite.Application.CreateClient();

        TestOutputStreamHolder.Current = TestContext.Out;
    }

    [TearDown]
    public void BaseAfterEachOfTheTests()
    {
        TestOutputStreamHolder.Current?.Flush();
        TestOutputStreamHolder.Current = null;
    }
}
