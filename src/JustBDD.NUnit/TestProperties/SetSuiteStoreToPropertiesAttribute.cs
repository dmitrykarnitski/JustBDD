using System.Linq;
using JustBDD.Core.Contexts.Stores;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace JustBDD.NUnit.TestProperties;

internal sealed class SetSuiteStoreToPropertiesAttribute : NUnitAttribute, IApplyToTest
{
    public void ApplyToTest(Test test)
    {
        SetSuiteStoreRecursively(test);
    }

    private void SetSuiteStoreRecursively(ITest test)
    {
        if (test.Tests.Any())
        {
            foreach (var testItem in test.Tests)
            {
                SetSuiteStoreRecursively(testItem);
            }
        }
        else
        {
            test.Properties.SetSuiteStore(SuiteStore.Instance);
        }
    }
}
