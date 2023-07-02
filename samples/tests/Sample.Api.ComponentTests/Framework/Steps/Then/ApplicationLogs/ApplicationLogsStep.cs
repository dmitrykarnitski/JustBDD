using FluentAssertions;
using JustBDD.Core;
using Sample.Api.ComponentTests.Framework.Steps.Base;

namespace Sample.Api.ComponentTests.Framework.Steps.Then.ApplicationLogs;

public class ApplicationLogsStep : Step<ThenStep>
{
    public IAnd<ThenStep> WillContainAnEntryFor(string message)
    {
        Scenario.ApplicationLogs.Entries.Should().Contain(entry => entry.OriginalFormat == message);

        return this;
    }
}
