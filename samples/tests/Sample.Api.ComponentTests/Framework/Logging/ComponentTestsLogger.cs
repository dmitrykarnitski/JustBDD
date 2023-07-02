using System;
using Microsoft.Extensions.Logging;
using Sample.Api.ComponentTests.Framework.BddContexts;

#pragma warning disable CA1063 // there is nothing to dispose in this class

namespace Sample.Api.ComponentTests.Framework.Logging;

internal class ComponentTestsLogger : ILogger, IDisposable
{
    private readonly string _loggerCategoryName;
    private readonly LoggerScopeFilter _loggerScopeFilter;
    private readonly Scenario _scenario;
    private readonly int _indent;
    private readonly object? _scope;

    public ComponentTestsLogger(string loggerCategoryName, Scenario scenario)
    {
        _loggerCategoryName = loggerCategoryName;
        _loggerScopeFilter = new LoggerScopeFilter(loggerCategoryName);
        _scenario = scenario;
    }

    public ComponentTestsLogger(string loggerCategoryName, Scenario scenario, int indent, object scope)
        : this(loggerCategoryName, scenario)
    {
        _indent = indent;
        _scope = scope;
    }

    public IDisposable BeginScope<TState>(TState state)
        where TState : notnull
    {
        return new ComponentTestsLogger(_loggerCategoryName, _scenario, _indent + 4, state);
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return _loggerScopeFilter.IsEnabled(logLevel);
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var entry = new LogEntry(logLevel, formatter(state, exception), state, _scope, exception, DateTime.UtcNow, _indent);

        _scenario.ApplicationLogs.Add(entry);
    }

    public void Dispose()
    {
    }
}
