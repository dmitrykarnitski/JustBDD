using System;
using Microsoft.Extensions.Logging;

namespace Sample.Api.Api.RequestPipeline.ErrorHandling;

public static partial class LoggerExtensions
{
    public const string UnhandledErrorMessageFormat = "Unhandled error occurred. Path={Path}";

    [LoggerMessage(
        Level = LogLevel.Error,
        Message = UnhandledErrorMessageFormat)]
    public static partial void UnhandledErrorOccurred(
        this ILogger<UnhandledErrorHandlingMiddleware> logger, 
        string path, 
        Exception exception);
}
