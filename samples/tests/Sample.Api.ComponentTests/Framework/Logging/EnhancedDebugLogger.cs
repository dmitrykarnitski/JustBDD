using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

#pragma warning disable CA1063 // there is nothing to dispose in this class

namespace Sample.Api.ComponentTests.Framework.Logging;

public class EnhancedDebugLogger : ILogger, IDisposable
{
    private readonly string _loggerName;

    public EnhancedDebugLogger(string loggerName)
    {
        _loggerName = loggerName;
    }

    public IDisposable BeginScope<TState>(TState state)
        where TState : notnull
    {
        Debug.WriteLine(state.ToString());

        return new EnhancedDebugLogger(_loggerName);
    }

    public void Dispose()
    {
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        var message = formatter(state, exception);

        Debug.WriteLine(message);

        if (exception is not null)
        {
            Debug.WriteLine(exception);
        }
    }
}
