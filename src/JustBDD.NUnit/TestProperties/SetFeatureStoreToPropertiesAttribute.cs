using JustBDD.Core.Contexts.Stores;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace JustBDD.NUnit.TestProperties;

internal class SetFeatureStoreToPropertiesAttribute : NUnitAttribute, IApplyToTest
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
