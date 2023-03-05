using JustBDD.NUnit;
using JustBDD.NUnit.TestOutputFormatting;

namespace Sample.Api.Tests.InMemory.Integration.Framework.FixtureSupport;

public class SampleApiJustBddNUnitSettings : DefaultJustBddNUnitSettings
{
    public override IReadableTestNameProvider? ReadableTestNameProvider { get; }
        = new ReadableTestNameProvider(excludedTestClassNameParts: new[] { "HappyPath", "SadPath" });
}
