using System.Threading.Tasks;
using JustBDD.Core;
using JustBDD.Core.Contexts;
using JustBDD.NUnit.ContextHelpers;
using JustBDD.NUnit.TestOutputFormatting;
using JustBDD.NUnit.TestProperties;
using NUnit.Framework;

namespace JustBDD.NUnit;

#pragma warning disable CA1000 // 'static' keyword for methods is required to apply OneTimeTearDown attributes
#pragma warning disable CA1005 // FixtureBase requires 3 type arguments for GWT pattern implementation

[FormatTestName]
[SetSuiteStoreToProperties]
[SetFeatureStoreToProperties]
[SetScenarioStoreToProperties]
public class BddFixtureBase<TGiven, TWhen, TThen>
    where TGiven : RootStepBase<TGiven>, new()
    where TWhen : RootStepBase<TWhen>, new()
    where TThen : RootStepBase<TThen>, new()
{
    private FixtureContext _context = null!;

    public TGiven Given { get; private set; } = null!;

    public TWhen When { get; private set; } = null!;

    public TThen Then { get; private set; } = null!;

    [SetUp]
    public void BddFixtureBaseBeforeEachTest()
    {
        _context = new FixtureContext();

        Given = new TGiven();
        Given.Initialize(_context.FeatureStore, _context.ScenarioStore);

        When = new TWhen();
        When.Initialize(_context.FeatureStore, _context.ScenarioStore);

        Then = new TThen();
        Then.Initialize(_context.FeatureStore, _context.ScenarioStore);

        if (SettingsProvider.Settings.TestOutputHeaderProvider is not null)
        {
            TestContext.Progress.WriteLine(SettingsProvider.Settings.TestOutputHeaderProvider.CreateTestHeader());
        }

        TestContextInstance.Current = TestContext.CurrentContext;
    }

    [TearDown]
    public async Task BddFixtureBaseAfterEachOfTheTests()
    {
        TestContextInstance.Current = default;

        await _context.ScenarioStore.DisposeAsync();
    }

    [OneTimeTearDown]
    public static async Task BddFixtureBaseAfterAllTests()
    {
        var featureStore = TestContext.CurrentContext.GetFeatureStore();
        if (featureStore != null)
        {
            await featureStore.DisposeAsync();
        }
    }

    protected TScenario ScenarioFactory<TScenario>()
        where TScenario : ScenarioBase, new()
    {
        return ContextFactory.Create<TScenario>(_context.ScenarioStore);
    }

    protected TFeature FeatureFactory<TFeature>()
        where TFeature : FeatureBase, new()
    {
        return ContextFactory.Create<TFeature>(_context.FeatureStore);
    }
}
