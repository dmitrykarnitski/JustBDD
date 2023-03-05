using JustBDD.Core.Contexts.Stores;

namespace JustBDD.Core;

public abstract class RootStepBase<TStep> : StepBase<TStep> where TStep : RootStepBase<TStep>
{
    public void Initialize(IContextStore featureStore, IContextStore scenarioStore)
    {
        InitializeContextStores(featureStore, scenarioStore);
    }
}
