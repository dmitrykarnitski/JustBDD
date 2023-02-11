using JustBDD.Core;
using Sample.Api.Tests.InMemory.Integration.Framework.BddContexts;

namespace Sample.Api.Tests.InMemory.Integration.Framework.Steps.Base;

public class Step<TStep> : StepBase<TStep> where TStep : StepBase<TStep>
{
    internal Scenario Scenario => ScenarioFactory<Scenario>();

    internal Feature Feature => FeatureFactory<Feature>();

    internal Suite Suite => Suite.Instance;
}
