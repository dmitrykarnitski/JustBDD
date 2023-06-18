using System.Collections.Generic;
using System.IO;

namespace Sample.Api.ComponentTests.Framework.BddContexts.ScenarioHelpers;

public class ApplicationLogs
{
    private readonly List<string> _entries = new();

    public void Add(string entry)
    {
        _entries.Add(entry);
    }

    public void WriteTo(TextWriter output)
    {
        foreach (var entry in _entries)
        {
            output.WriteLine(entry);
        }
    }
}
