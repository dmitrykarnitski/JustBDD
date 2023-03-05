using JustBDD.Core.Contexts;
using JustBDD.Core.Contexts.Stores;

namespace JustBDD.Core;

public abstract class StepBase<TStep> : IAnd<TStep> where TStep : StepBase<TStep>
{
    private TStep? _and;

    public TStep And
    {
        get => (TStep)(_and ?? this);
        private set => _and = value;
    }

    internal IContextStore ScenarioStore { get; private set; } = null!;

    internal IContextStore FeatureStore { get; private set; } = null!;

    internal void InitializeContextStores(IContextStore featureStore, IContextStore scenarioStore)
    {
        FeatureStore = featureStore;
        ScenarioStore = scenarioStore;
    }

    public TScenario ScenarioFactory<TScenario>()
        where TScenario : ScenarioBase, new()
    {
        return ContextFactory.Create<TScenario>(ScenarioStore);
    }

    public TFeature FeatureFactory<TFeature>()
        where TFeature : FeatureBase, new()
    {
        return ContextFactory.Create<TFeature>(FeatureStore);
    }

    public TStepBuilder StepFactory<TStepBuilder>()
        where TStepBuilder : StepBase<TStep>, new()
    {
        var newStepBuilder = new TStepBuilder();

        newStepBuilder.CopyFrom(this);

        return newStepBuilder;
    }

    private void CopyFrom(StepBase<TStep> anotherStepBase)
    {
        ScenarioStore = anotherStepBase.ScenarioStore;
        FeatureStore = anotherStepBase.FeatureStore;

        And = anotherStepBase.And;
    }
}
