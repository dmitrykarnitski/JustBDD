using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Sample.Api.ComponentTests.Framework.Logging;

public static class LoggingBuilderExtensions
{
    public static ILoggingBuilder InterceptApplicationLogs(this ILoggingBuilder loggingBuilder)
    {
        loggingBuilder.ClearProviders();

        loggingBuilder.Services.AddSingleton<ILoggerFactory, ComponentTestsLoggerFactory>();

        return loggingBuilder;
    }
}
