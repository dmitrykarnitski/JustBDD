using System;
using Microsoft.Extensions.Logging;

namespace Sample.Api.Api.Attributes;

public static class LoggerExtensions
{
    private static readonly Func<ILogger, string, string, string, string?, IDisposable> executingControllerScopeAction =
        LoggerMessage.DefineScope<string, string, string, string?>(
            "Executing {ControllerName}.{ActionName}. Route: {HttpMethod} /{PathTemplate}");

    public static IDisposable ExecutingControllerScope(
        this ILogger logger,
        string controllerName,
        string actionName,
        string method,
        string? pathTemplate)
    {
        return executingControllerScopeAction(logger, controllerName, actionName, method, pathTemplate);
    }
}
