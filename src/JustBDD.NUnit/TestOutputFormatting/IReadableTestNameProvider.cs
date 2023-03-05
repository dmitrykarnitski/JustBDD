using NUnit.Framework.Interfaces;

namespace JustBDD.NUnit.TestOutputFormatting;

public interface IReadableTestNameProvider
{
    string? GetReadableTestName(ITest test);
}
