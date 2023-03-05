using Microsoft.Extensions.Logging;

#pragma warning disable CA1063 // there is nothing to dispose in this class

namespace Sample.Api.ComponentTests.Framework.Logging;

public class EnhancedDebugLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new EnhancedDebugLogger(categoryName);
    }

    public void Dispose()
    {
    }
}
