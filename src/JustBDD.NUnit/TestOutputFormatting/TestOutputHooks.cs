using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace JustBDD.NUnit.TestOutputFormatting;

public class TestOutputHooks : ITestOutputHooks
{
    public async Task BeforeTestAsync(TextWriter output)
    {
        await output.WriteLineAsync(new string('-', 100));

        await output.WriteLineAsync($"Executing test: {TestContext.CurrentContext.GetRawTestName()}");

        var readableTestName = GetReadableTestName();
        if (!string.IsNullOrWhiteSpace(readableTestName))
        {
            await output.WriteLineAsync($"Formatted name: {readableTestName}");
        }

        var testArguments = TestContext.CurrentContext.GetFormattedTestArguments();
        if (testArguments.Any())
        {
            await output.WriteLineAsync("Arguments: ");

            foreach (var argument in testArguments)
            {
                await output.WriteAsync(new string(' ', 4));
                await output.WriteLineAsync(argument);
            }
        }

        await output.WriteLineAsync($"Started at: {DateTime.UtcNow:O}");

        await output.WriteLineAsync(new string('-', 10));
    }

    public async Task AfterTestAsync(TextWriter output)
    {
        await output.WriteLineAsync(new string('-', 10));

        await output.WriteLineAsync($"Finished at: {DateTime.UtcNow:O}");

        await output.WriteLineAsync(new string('-', 100));
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
