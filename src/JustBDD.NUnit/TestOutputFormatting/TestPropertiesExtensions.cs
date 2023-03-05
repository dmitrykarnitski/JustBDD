using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace JustBDD.NUnit.TestOutputFormatting;

public static class TestPropertiesExtensions
{
    private const string FormattedTestNameKey = "FormattedTestName";
    private const string FormattedTestArgumentsKey = "FormattedTestArguments";

    public static string? GetFormattedTestName(this TestContext testContext)
    {
        return testContext.Test.Properties.Get(FormattedTestNameKey) as string;
    }

    public static IReadOnlyCollection<string> GetFormattedTestArguments(this TestContext testContext)
    {
        var arguments = testContext.Test.Properties.Get(FormattedTestArgumentsKey) as IReadOnlyCollection<string>;

        return arguments ?? Array.Empty<string>();
    }

    public static string GetRawTestName(this TestContext testContext)
    {
        var shortClassName = testContext.Test.ClassName!.Split('.').Last();

        return $"{shortClassName}.{testContext.Test.MethodName}";
    }

    internal static void SetFormattedTestName(this ITest test, string testCaseName)
    {
        test.Properties.Set(FormattedTestNameKey, testCaseName);
    }

    internal static void SetFormattedTestArguments(this ITest test, IReadOnlyCollection<string> testArguments)
    {
        test.Properties.Set(FormattedTestArgumentsKey, testArguments);
    }
}
