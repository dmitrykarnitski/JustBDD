using System.Linq;
using System.Text;
using NUnit.Framework;

namespace JustBDD.NUnit.TestOutputFormatting;

public class TestOutputHeaderProvider : ITestOutputHeaderProvider
{
    public string CreateTestHeader()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine(new string('-', 100));
        stringBuilder.AppendLine(new string('-', 100));

        stringBuilder.AppendLine($"Executing test: {TestContext.CurrentContext.GetRawTestName()}");

        var readableTestName = GetReadableTestName();
        if (!string.IsNullOrWhiteSpace(readableTestName))
        {
            stringBuilder.AppendLine($"Formatted name: {readableTestName}");
        }

        var testArguments = TestContext.CurrentContext.GetFormattedTestArguments();
        if (testArguments.Any())
        {
            stringBuilder.AppendLine("Arguments: ");

            foreach (var argument in testArguments)
            {
                stringBuilder.Append(new string(' ', 4));
                stringBuilder.AppendLine(argument);
            }
        }

        stringBuilder.AppendLine(new string('-', 10));

        return stringBuilder.ToString();
    }

    protected virtual string? GetReadableTestName()
    {
        var formattedTestName = TestContext.CurrentContext.GetFormattedTestName();
        if (!string.IsNullOrWhiteSpace(formattedTestName))
        {
            return formattedTestName;
        }

        return TestContext.CurrentContext.Test.Properties.Get("Description") as string;
    }
}
