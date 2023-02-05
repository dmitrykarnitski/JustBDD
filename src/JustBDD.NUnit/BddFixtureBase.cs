using JustBDD.Core;
using JustBDD.Core.Contexts;
using JustBDD.Core.Contexts.Stores;
using JustBDD.NUnit.ContextHelpers;
using JustBDD.NUnit.TestProperties;
using NUnit.Framework;

namespace JustBDD.NUnit;

#pragma warning disable CA1000 // 'static' keyword is required to apply OneTimeTearDown attributes
#pragma warning disable CA1005 // FixtureBase requires 3 type arguments for GWT pattern implementation

[SetSuiteStoreToProperties]
[SetFeatureStoreToProperties]
[SetScenarioStoreToProperties]
public class BddFixtureBase<TGiven, TWhen, TThen>
    where TGiven : RootStepBase<TGiven>, new()
    where TWhen : RootStepBase<TWhen>, new()
    where TThen : RootStepBase<TThen>, new()
{
    public TGiven Given { get; private set; } = null!;

    public TWhen When { get; private set; } = null!;

    public TThen Then { get; private set; } = null!;

    internal FixtureContext Context { get; private set; } = null!;

    [SetUp]
    public void BddFixtureBaseBeforeEachTest()
    {
        TestContextInstance.Current = TestContext.CurrentContext;

        var featureStore = TestContextInstance.Current.Test.Properties.GetFeatureStore();
        var scenarioStore = TestContextInstance.Current.Test.Properties.GetScenarioStore();

        Context = new FixtureContext(SuiteStore.Instance, featureStore, scenarioStore);

        Given = new TGiven();
        Given.Initialize(Context.FeatureStore, Context.ScenarioStore);

        When = new TWhen();
        When.Initialize(Context.FeatureStore, Context.ScenarioStore);

        Then = new TThen();
        Then.Initialize(Context.FeatureStore, Context.ScenarioStore);
    }

    [TearDown]
    public void BddFixtureBaseAfterEachOfTheTests()
    {
        TestContextInstance.Current = default;

        Context?.ScenarioStore?.Dispose();
    }

    [OneTimeTearDown]
    public static void BddFixtureBaseAfterAllTests()
    {
        var featureStore = TestContext.CurrentContext.Test.Properties.GetFeatureStore();
        featureStore.Dispose();
    }

    protected TScenario ScenarioFactory<TScenario>() where TScenario : ScenarioBase, new()
    {
        return ContextFactory.Create<TScenario>(Context.ScenarioStore);
    }

    protected TFeature FeatureFactory<TFeature>() where TFeature : FeatureBase, new()
    {
        return ContextFactory.Create<TFeature>(Context.FeatureStore);
    }
}

#pragma warning restore CA1005
#pragma warning restore CA1000
