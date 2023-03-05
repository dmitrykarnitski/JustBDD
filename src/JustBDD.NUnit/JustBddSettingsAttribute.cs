using System;
using JustBDD.NUnit.TestOutputFormatting;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace JustBDD.NUnit;

[AttributeUsage(AttributeTargets.Class)]
public sealed class JustBddSettingsAttribute : NUnitAttribute, IApplyToTest
{
    public JustBddSettingsAttribute(Type settingsType)
    {
        if (!settingsType.IsAssignableTo(typeof(IJustBddNUnitSettings)))
        {
            throw new ArgumentException($"Settings should implement {nameof(IJustBddNUnitSettings)} interface.", nameof(settingsType));
        }

        SettingsType = settingsType;
    }

    public Type SettingsType { get; }

    public void ApplyToTest(Test test)
    {
        var settings = (IJustBddNUnitSettings?)Activator.CreateInstance(SettingsType);

        if (settings is null)
        {
            throw new InvalidOperationException(
                $"Unable to create an instance of settings of type {SettingsType.Name}.");
        }

        SettingsProvider.Settings = settings;
    }
}
