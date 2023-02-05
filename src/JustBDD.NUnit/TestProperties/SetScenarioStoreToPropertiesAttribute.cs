using JustBDD.Core.Contexts.Stores;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace JustBDD.NUnit.TestProperties;

internal sealed class SetScenarioStoreToPropertiesAttribute : NUnitAttribute, IApplyToTest
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Context store is disposed at the tear down stage.")]
    public void ApplyToTest(Test test)
    {
        // TODO: handle theory tests
        if (test is not TestFixture testFixture)
        {
            return;
        }

        foreach (var testItem in testFixture.Tests)
        {
            var scenarioStore = new ContextStore();
            testItem.Properties.SetScenarioStore(scenarioStore);
        }
    }
}
