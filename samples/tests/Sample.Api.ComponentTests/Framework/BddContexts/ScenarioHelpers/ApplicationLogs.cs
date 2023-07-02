using System.Collections.Generic;
using System.IO;
using Sample.Api.ComponentTests.Framework.Logging;

namespace Sample.Api.ComponentTests.Framework.BddContexts.ScenarioHelpers;

public class ApplicationLogs
{
    private readonly List<LogEntry> _entries = new();

    public IReadOnlyCollection<LogEntry> Entries => _entries;

    public void Add(LogEntry entry)
    {
        _entries.Add(entry);
    }

    public void WriteTo(TextWriter output)
    {
        foreach (var entry in _entries)
        {
            entry.WriteTo(output);
        }
    }
}
