using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace JustBDD.NUnit.TestNameFormatting;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class FormatTestNameAttribute : NUnitAttribute, IApplyToTest
{
    public void ApplyToTest(Test test)
    {
        // TODO: handle theory tests
        if (test is not TestFixture testFixture)
        {
            return;
        }

        var testPrefix = GetTestPrefix(testFixture.TypeInfo);

        foreach (var testItem in testFixture.Tests)
        {
            if (testItem is TestMethod testMethod)
            {
                var testName = GetTestName(testMethod.Method);

                testMethod.Name = $"{testPrefix} {testName} [{testFixture.TypeInfo.Name}.{testMethod.MethodName}]";
            }
        }
    }

    private string GetTestPrefix(ITypeInfo testFixture)
    {
        var description = GetDescriptionAttributeValue(testFixture);
        if (!string.IsNullOrWhiteSpace(description))
        {
            return description;
        }

        var classNamePrefix = testFixture.Name.Split('_').First();

        var classNameSplitInWords = SplitIntoWords(classNamePrefix);

        return $"{ConvertToSentenseCase(classNameSplitInWords)} should";
    }

    private string GetTestName(IMethodInfo testMethod)
    {
        var description = GetDescriptionAttributeValue(testMethod);
        if (!string.IsNullOrWhiteSpace(description))
        {
            return description;
        }

        var methodNameSplitInWords = SplitIntoWords(testMethod.Name);

        var testName = ConvertToSentenseCase(methodNameSplitInWords);

        return char.ToLowerInvariant(testName[0]) + testName.Substring(1);
    }

    private string SplitIntoWords(string input)
    {
        var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

        return r.Replace(input, " ");
    }

    private string ConvertToSentenseCase(string input)
    {
        var lowerCase = input.ToLower();

        var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture);

        return r.Replace(lowerCase, s => s.Value.ToUpper());
    }

    private string? GetDescriptionAttributeValue(IReflectionInfo reflectionInfo)
    {
        return reflectionInfo.GetCustomAttributes<DescriptionAttribute>(inherit: true)
            .FirstOrDefault()
            ?.Properties
            .Get("Description")
            ?.ToString();
    }
}
