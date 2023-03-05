using JustBDD.Core;
using Sample.Api.ComponentTests.Framework.BddContexts;

namespace Sample.Api.ComponentTests.Framework.Steps.Base;

public class Step<TStep> : StepBase<TStep> where TStep : StepBase<TStep>
{
    internal Scenario Scenario => ScenarioFactory<Scenario>();

    internal Feature Feature => FeatureFactory<Feature>();

    internal Suite Suite => Suite.Instance;
}
