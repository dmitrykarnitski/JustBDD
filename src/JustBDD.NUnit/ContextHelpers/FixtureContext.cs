using JustBDD.Core.Contexts.Stores;

namespace JustBDD.NUnit.ContextHelpers;

internal class FixtureContext
{
    public FixtureContext(ContextStore suiteContextStore, ContextStore featureContextStore, ContextStore scenarioContextStore)
    {
        SuiteContextStore = suiteContextStore;
        FeatureStore = featureContextStore;
        ScenarioStore = scenarioContextStore;
    }

    public ContextStore SuiteContextStore { get; }

    public ContextStore FeatureStore { get; }

    public ContextStore ScenarioStore { get; }
}
