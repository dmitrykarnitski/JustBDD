using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Sample.Api.ComponentTests.Framework.Logging;

public class LoggerScopeFilter
{
    private readonly string _loggerCategoryName;

    private static readonly List<Filter> filters = new()
    {
        Filter.NoneFor("Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware"),
        Filter.ErrorAndHigherFor("Microsoft.AspNetCore.Hosting.Diagnostics"),
        Filter.ErrorAndHigherFor("Microsoft.AspNetCore.Routing.EndpointMiddleware"),
        Filter.ErrorAndHigherFor("Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager"),
        Filter.ErrorAndHigherFor("Microsoft.AspNetCore.Mvc.Infrastructure."),
        Filter.AllFor("Sample."), // application logs
        Filter.Default(LogLevel.Information) // default filter
    };

    public LoggerScopeFilter(string loggerCategoryName)
    {
        _loggerCategoryName = loggerCategoryName;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        var matchedFilter = filters.First(x => _loggerCategoryName.StartsWith((string)x.Prefix));

        return (int)logLevel >= (int)matchedFilter.LogLevel;
    }

    private class Filter
    {
        public string Prefix { get; }

        public LogLevel LogLevel { get; }

        private Filter(string prefix, LogLevel logLevel)
        {
            Prefix = prefix;
            LogLevel = logLevel;
        }

        public static Filter Default(LogLevel logLevel)
        {
            return new Filter(string.Empty, logLevel);
        }

        public static Filter NoneFor(string prefix)
        {
            return new Filter(prefix, LogLevel.None);
        }

        public static Filter AllFor(string prefix)
        {
            return new Filter(prefix, LogLevel.Trace);
        }

        public static Filter ErrorAndHigherFor(string prefix)
        {
            return new Filter(prefix, LogLevel.Error);
        }
    }
}