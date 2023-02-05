using JustBDD.Core.Contexts.Stores;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace JustBDD.NUnit.TestProperties;

internal sealed class SetSuiteStoreToPropertiesAttribute : NUnitAttribute, IApplyToTest
{
    public void ApplyToTest(Test test)
    {
        test.Properties.SetSuiteStore(SuiteStore.Instance);

        // TODO: handle theory tests
        if (test is not TestFixture testFixture)
        {
            return;
        }

        foreach (var testItem in testFixture.Tests)
        {
            testItem.Properties.SetSuiteStore(SuiteStore.Instance);
        }
    }
}
