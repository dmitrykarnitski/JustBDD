using System;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace JustBDD.NUnit.TestOutputFormatting;

[AttributeUsage(AttributeTargets.Class)]
internal sealed class FormatTestNameAttribute : NUnitAttribute, IApplyToTest
{
    public void ApplyToTest(Test test)
    {
        if (SettingsProvider.Settings.ReadableTestNameProvider is null && SettingsProvider.Settings.TestArgumentsFormatter is null)
        {
            return;
        }

        SetTestNameRecursively(test);
    }

    private void SetTestNameRecursively(ITest test)
    {
        if (test.Tests.Any())
        {
            foreach (var testItem in test.Tests)
            {
                SetTestNameRecursively(testItem);
            }
        }
        else
        {
            var testName = SettingsProvider.Settings.ReadableTestNameProvider?.GetReadableTestName(test);
            if (testName is not null)
            {
                test.SetFormattedTestName(testName);
            }

            var testArguments = SettingsProvider.Settings.TestArgumentsFormatter?.FormatArguments(test);
            if (testArguments is not null)
            {
                test.SetFormattedTestArguments(testArguments);
            }
        }
    }
}
