using JustBDD.Core.Contexts;

namespace Sample.Api.ComponentTests.Framework.BddContexts;

/// <summary>
/// Holds shared data that is scoped to the group of tests.
/// </summary>
/// <remarks>
/// <see cref="Suite"/>, <see cref="Feature"/> and <see cref="Scenario"/> classes are defined as internal as they may hold references to internal classes from the SUT.
/// </remarks>
internal class Feature : FeatureBase
{
}
