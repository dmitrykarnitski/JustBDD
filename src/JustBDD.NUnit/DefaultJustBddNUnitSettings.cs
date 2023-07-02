using JustBDD.NUnit.TestOutputFormatting;

namespace JustBDD.NUnit;

public class DefaultJustBddNUnitSettings : IJustBddNUnitSettings
{
    public virtual ITestArgumentsFormatter? TestArgumentsFormatter { get; } = new TestArgumentsFormatter();

    public virtual IReadableTestNameProvider? ReadableTestNameProvider { get; } = new ReadableTestNameProvider();

    public virtual ITestOutputHooks? TestOutputHooks { get; } = new TestOutputHooks();
}
