using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Sample.Api.ComponentTests.Framework.BddContexts;

#pragma warning disable CA1063 // there is nothing to dispose in this class

namespace Sample.Api.ComponentTests.Framework.Logging;

internal class EnhancedDebugLogger : ILogger, IDisposable
{
    private static readonly IDictionary<string, LogLevel> filters = new Dictionary<string, LogLevel>
    {
        ["Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware"] = LogLevel.None,
        ["Microsoft.AspNetCore.Hosting.Diagnostics"] = LogLevel.Error,
        ["Microsoft.AspNetCore.Routing.EndpointMiddleware"] = LogLevel.Error,
        ["Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager"] = LogLevel.Error,
        ["Microsoft.AspNetCore.Mvc.Infrastructure."] = LogLevel.Error,
    };

    private readonly string _loggerName;
    private readonly Scenario _scenario;

    public EnhancedDebugLogger(string loggerName, Scenario scenario)
    {
        _loggerName = loggerName;
        _scenario = scenario;
    }

    public IDisposable BeginScope<TState>(TState state)
        where TState : notnull
    {
        return new EnhancedDebugLogger(_loggerName, _scenario);
    }

    public void Dispose()
    {
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        var matchedFilter = filters.FirstOrDefault(x => _loggerName.StartsWith(x.Key));
        if (!string.IsNullOrWhiteSpace(matchedFilter.Key))
        {
            return (int)logLevel >= (int)matchedFilter.Value;
        }

        return (int)logLevel >= (int)LogLevel.Information;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var message = formatter(state, exception);

        _scenario.ApplicationLogs.Add(message);
    }
}
