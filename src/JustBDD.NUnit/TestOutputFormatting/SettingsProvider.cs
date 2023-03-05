namespace JustBDD.NUnit.TestOutputFormatting;

internal static class SettingsProvider
{
    public static IJustBddNUnitSettings Settings { get; set; } = new DefaultJustBddNUnitSettings();
}