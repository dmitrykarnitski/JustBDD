using System.Collections.Generic;
using NUnit.Framework.Interfaces;

namespace JustBDD.NUnit.TestOutputFormatting;

public interface ITestArgumentsFormatter
{
    IReadOnlyCollection<string> FormatArguments(ITest test);
}
