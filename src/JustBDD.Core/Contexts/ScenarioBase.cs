using System;

namespace JustBDD.Core.Contexts;

/// <summary>
/// Marker class for Scenario context.
/// </summary>
public class ScenarioBase : TestContextBase
{
    public string Id => GetAndInitialiseIfNotSet(nameof(Id), () => Guid.NewGuid().ToString());
}
