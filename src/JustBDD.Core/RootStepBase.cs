using JustBDD.Core.Contexts.Stores;

namespace JustBDD.Core;

public abstract class RootStepBase<TStep> : StepBase<TStep> where TStep : RootStepBase<TStep>
{
    public void Initialize(ContextStore featureStore, ContextStore scenarioStore)
    {
        InitializeContextStores(featureStore, scenarioStore);
    }
}
