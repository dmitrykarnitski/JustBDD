using Microsoft.Extensions.Logging;
using Sample.Api.ComponentTests.Framework.BddContexts;

#pragma warning disable CA1063 // there is nothing to dispose in this class

namespace Sample.Api.ComponentTests.Framework.Logging;

internal class ComponentTestsLoggerFactory : ILoggerFactory
{
    private readonly Scenario _scenario;

    public ComponentTestsLoggerFactory(Scenario scenario)
    {
        _scenario = scenario;
    }

    public void AddProvider(ILoggerProvider provider)
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new ComponentTestsLogger(categoryName, _scenario);
    }

    public void Dispose()
    {
    }
}
