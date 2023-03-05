using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace JustBDD.NUnit.TestOutputFormatting;

public class ReadableTestNameProvider : IReadableTestNameProvider
{
    private static readonly Regex wordsSeparatorRegex = new(
        @"(?<=[A-Z])(?=[A-Z][a-z]) | (?<=[^A-Z])(?=[A-Z]) | (?<=[A-Za-z])(?=[^A-Za-z])",
        RegexOptions.IgnorePatternWhitespace);

    private static readonly Regex sentenceCaseConvertorRegex = new(
        @"(^[a-z])|\.\s+(.)",
        RegexOptions.ExplicitCapture);


    private readonly IReadOnlyCollection<string> _excludedTestClassNameParts;
    private readonly string? _prefixPartsSeparator;

    public ReadableTestNameProvider()
        : this(Array.Empty<string>(), "_")
    {
    }

    public ReadableTestNameProvider(IEnumerable<string> excludedTestClassNameParts)
        : this(excludedTestClassNameParts, "_")
    {
    }

    public ReadableTestNameProvider(IEnumerable<string> excludedTestClassNameParts, string prefixPartsSeparator)
    {
        _excludedTestClassNameParts = excludedTestClassNameParts.ToArray();
        _prefixPartsSeparator = prefixPartsSeparator;
    }

    public string? GetReadableTestName(ITest test)
    {
        if (test is not TestMethod testMethod)
        {
            return null;
        }

        var testPrefix = GetTestPrefix(testMethod);

        var testName = GetTestName(testMethod);

        if (testPrefix is null)
        {
            return testName;
        }

        return $"{testPrefix} {testName}";
    }

    protected virtual string? GetTestPrefix(TestMethod testMethod)
    {
        if (testMethod.TypeInfo is null)
        {
            return null;
        }

        var description = GetDescriptionAttributeValue(testMethod.TypeInfo);
        if (!string.IsNullOrWhiteSpace(description))
        {
            return description;
        }

        return CreateTestPrefix(testMethod);
    }

    protected virtual string CreateTestPrefix(TestMethod testMethod)
    {
        string notConvertedTestPrefix;
        var className = testMethod.TypeInfo!.Name;

        if (string.IsNullOrWhiteSpace(_prefixPartsSeparator))
        {
            notConvertedTestPrefix = _excludedTestClassNameParts.Aggregate(className,
                (testPrefix, excludedTestClassNamePart) =>
                    testPrefix.Replace(excludedTestClassNamePart, string.Empty, StringComparison.Ordinal));
        }
        else
        {
            var prefixParts = className
                .Split(_prefixPartsSeparator, StringSplitOptions.RemoveEmptyEntries)
                .Where(prefixPart => !_excludedTestClassNameParts.Contains(prefixPart));

            notConvertedTestPrefix = string.Join(string.Empty, prefixParts);
        }

        var testPrefixSplitIntoWords = SplitIntoWords(notConvertedTestPrefix);

        return ConvertToSentenceCase(testPrefixSplitIntoWords);
    }

    private string GetTestName(TestMethod testMethod)
    {
        var description = GetDescriptionAttributeValue(testMethod.Method);
        if (!string.IsNullOrWhiteSpace(description))
        {
            return description;
        }

        var methodNameSplitInWords = SplitIntoWords(testMethod.MethodName);

        var testName = ConvertToSentenceCase(methodNameSplitInWords);

        return char.ToLowerInvariant(testName[0]) + testName[1..];
    }

    private string SplitIntoWords(string input)
    {
        return wordsSeparatorRegex.Replace(input, " ");
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "Only English alphabet lowercase letters are supported.")]
    private string ConvertToSentenceCase(string input)
    {
        var lowerCase = input.ToLowerInvariant();

        return sentenceCaseConvertorRegex.Replace(lowerCase, s => s.Value.ToUpperInvariant());
    }

    private string? GetDescriptionAttributeValue(IReflectionInfo reflectionInfo)
    {
        return reflectionInfo.GetCustomAttributes<DescriptionAttribute>(inherit: true)
            .FirstOrDefault()
            ?.Properties
            .Get("Description") as string;
    }
}
