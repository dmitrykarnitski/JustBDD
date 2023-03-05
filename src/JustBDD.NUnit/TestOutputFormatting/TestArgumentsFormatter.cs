using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace JustBDD.NUnit.TestOutputFormatting;

public class TestArgumentsFormatter : ITestArgumentsFormatter
{
    protected string NullPlaceholder { get; set; } = "(null)";
    protected string EmptyStringPlaceholder { get; set; } = "(empty string)";
    protected string EmptyCollectionPlaceholder { get; set; } = "(empty collection)";
    protected string WhitespacePlaceholder { get; set; } = "(whitespace)";

    public IReadOnlyCollection<string> FormatArguments(ITest test)
    {
        if (test is not TestMethod testMethod)
        {
            return Array.Empty<string>();
        }

        var parameters = testMethod.Method.MethodInfo.GetParameters();

        return test.Arguments
            .Select((argumentValue, index) => FormatArgument(parameters[index], argumentValue))
            .ToArray();
    }

    protected virtual string FormatArgument(ParameterInfo parameterInfo, object? argumentValue)
    {
        return $"{parameterInfo.Name}: {FormatArgumentValue(argumentValue)}";
    }

    protected virtual string FormatArgumentValue(object? argumentValue)
    {
        if (argumentValue is null)
        {
            return NullPlaceholder;
        }

        if (argumentValue is "")
        {
            return EmptyStringPlaceholder;
        }

        if (string.IsNullOrWhiteSpace(argumentValue as string))
        {
            return WhitespacePlaceholder;
        }

        if (argumentValue is IEnumerable enumerable && !enumerable.GetEnumerator().MoveNext())
        {
            return EmptyCollectionPlaceholder;
        }

        return FormatObjectTypeArgument(argumentValue);
    }

    protected virtual string FormatObjectTypeArgument(object? argumentValue)
    {
        return JsonSerializer.Serialize(argumentValue);
    }
}
