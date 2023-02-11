using JustBDD.NUnit;
using JustBDD.NUnit.TestNameFormatting;
using NUnit.Framework;
using Sample.Api.Tests.InMemory.Integration.Framework.BddContexts;
using Sample.Api.Tests.InMemory.Integration.Framework.Logging;
using Sample.Api.Tests.InMemory.Integration.Framework.Steps.Given;
using Sample.Api.Tests.InMemory.Integration.Framework.Steps.Then;
using Sample.Api.Tests.InMemory.Integration.Framework.Steps.When;

namespace Sample.Api.Tests.InMemory.Integration.Framework.FixtureSupport;

[FormatTestName]
public class TestFixtureBase : BddFixtureBase<GivenStep, WhenStep, ThenStep>
{
    internal Suite Suite { get; private set; }

    internal Feature Feature { get; private set; }

    internal Scenario Scenario { get; private set; }

    [SetUp]
    public void BaseBeforeEachTest()
    {
        Suite = Suite.Instance;
        Feature = FeatureFactory<Feature>();
        Scenario = ScenarioFactory<Scenario>();

        Scenario.SutHttpClient = Suite.Application.CreateClient();

        TestContextInstance.Current = TestContext.CurrentContext;
        TestOutputStreamHolder.Current = TestContext.Out;
    }

    [TearDown]
    public void BaseAfterEachOfTheTests()
    {
        TestOutputStreamHolder.Current?.Flush();
        TestOutputStreamHolder.Current = null;
    }
}
