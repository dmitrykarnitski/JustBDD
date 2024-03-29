﻿using JustBDD.NUnit.TestOutputFormatting;

namespace JustBDD.NUnit;

public interface IJustBddNUnitSettings
{
    ITestArgumentsFormatter? TestArgumentsFormatter { get; }

    IReadableTestNameProvider? ReadableTestNameProvider { get; }

    ITestOutputHeaderProvider? TestOutputHeaderProvider { get; }
}
