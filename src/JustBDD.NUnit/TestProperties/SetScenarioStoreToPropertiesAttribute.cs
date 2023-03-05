using System.Linq;
using JustBDD.Core.Contexts;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace JustBDD.NUnit.TestProperties;

#pragma warning disable CA2000 // Context store is disposed at the tear down stage

internal sealed class SetScenarioStoreToPropertiesAttribute : NUnitAttribute, IApplyToTest
{
    public void ApplyToTest(Test test)
    {
        SetScenarioStoreRecursively(test);
    }

    private void SetScenarioStoreRecursively(ITest test)
    {
        if (test.Tests.Any())
        {
            foreach (var testItem in test.Tests)
            {
                SetScenarioStoreRecursively(testItem);
            }
        }
        else
        {
            test.Properties.SetScenarioStore(ContextStoreFactory.Create());
        }
    }
}
