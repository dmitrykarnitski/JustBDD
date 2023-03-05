using JustBDD.Core.Contexts.Stores;
using JustBDD.NUnit.TestProperties;
using NUnit.Framework;

namespace JustBDD.NUnit.ContextHelpers;

internal class FixtureContext
{
    public FixtureContext()
    {
        SuiteContextStore = SuiteStore.Instance;
        FeatureStore = TestContext.CurrentContext.GetFeatureStore()!;
        ScenarioStore = TestContext.CurrentContext.GetScenarioStore()!;
    }

    public IContextStore SuiteContextStore { get; }

    public IContextStore FeatureStore { get; }

    public IContextStore ScenarioStore { get; }
}
