using JustBDD.Core;
using JustBDD.Core.Contexts;
using JustBDD.Core.Contexts.Stores;
using JustBDD.NUnit.ContextHelpers;
using JustBDD.NUnit.TestProperties;
using NUnit.Framework;

namespace JustBDD.NUnit;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

[TestFixture]
[SetSuiteStoreToProperties]
[SetFeatureStoreToProperties]
[SetScenarioStoreToProperties]
public class BddFixtureBase<TGiven, TWhen, TThen>
    where TGiven : RootStepBase<TGiven>, new()
    where TWhen : RootStepBase<TWhen>, new()
    where TThen : RootStepBase<TThen>, new()
{

    public TGiven Given { get; private set; }

    public TWhen When { get; private set; }

    public TThen Then { get; private set; }

    internal FixtureContext Context { get; private set; }

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
        featureStore?.Dispose();
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

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
