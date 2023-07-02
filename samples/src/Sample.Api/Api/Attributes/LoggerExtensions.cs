using Microsoft.Extensions.Logging;

namespace Sample.Api.Api.Attributes;

public static partial class LoggerExtensions
{
    public const string ControllerNamePlaceholder = "ControllerName";
    public const string ActionNamePlaceholder = "ActionName";
    public const string HttpMethodPlaceholder = "HttpMethod";
    public const string PathTemplatePlaceholder = "PathTemplate";

    [LoggerMessage(
        Level = LogLevel.Information,
        Message = $"Executing {{{ControllerNamePlaceholder}}}.{{{ActionNamePlaceholder}}}. Route: {{{HttpMethodPlaceholder}}} {{{PathTemplatePlaceholder}}}")]
    public static partial void LogActionExecution(
        this ILogger<LogActionExecutionAttribute> logger,
        string controllerName,
        string actionName,
        string httpMethod,
        string pathTemplate);
}
