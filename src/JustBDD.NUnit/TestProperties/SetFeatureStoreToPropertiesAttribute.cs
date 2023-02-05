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
        var featureStore = new ContextStore();

        test.Properties.SetFeatureStore(featureStore);

        // TODO: handle theory tests
        if (test is not TestFixture testFixture)
        {
            return;
        }

        foreach (var testItem in testFixture.Tests)
        {
            testItem.Properties.SetFeatureStore(featureStore);
        }
    }
}

#pragma warning restore CA2000
