using System.IO;
using System.Threading.Tasks;

namespace JustBDD.NUnit.TestOutputFormatting;

public interface ITestOutputHooks
{
    Task BeforeTestAsync(TextWriter output);

    Task AfterTestAsync(TextWriter output);
}
