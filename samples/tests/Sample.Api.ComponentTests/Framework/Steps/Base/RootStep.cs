using JustBDD.Core;
using Sample.Api.ComponentTests.Framework.BddContexts;

namespace Sample.Api.ComponentTests.Framework.Steps.Base;

public class RootStep<TStep> : RootStepBase<TStep> where TStep : RootStep<TStep>
{
    internal Scenario Scenario => ScenarioFactory<Scenario>();

    internal Feature Feature => FeatureFactory<Feature>();

    internal Suite Suite => Suite.Instance;
}
