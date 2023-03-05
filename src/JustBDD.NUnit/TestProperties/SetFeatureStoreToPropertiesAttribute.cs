using System.Linq;
using JustBDD.Core.Contexts;
using JustBDD.Core.Contexts.Stores;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace JustBDD.NUnit.TestProperties;

#pragma warning disable CA2000 // ContextStore is disposed as part of teardown

internal sealed class SetFeatureStoreToPropertiesAttribute : NUnitAttribute, IApplyToTest
{
    public void ApplyToTest(Test test)
    {
        var featureStore = ContextStoreFactory.Create();

        SetFeatureStoreRecursively(test, featureStore);
    }

    private void SetFeatureStoreRecursively(ITest test, IContextStore featureStore)
    {
        if (test.Tests.Any())
        {
            foreach (var testItem in test.Tests)
            {
                SetFeatureStoreRecursively(testItem, featureStore);
            }
        }
        else
        {
            test.Properties.SetFeatureStore(featureStore);
        }
    }
}
