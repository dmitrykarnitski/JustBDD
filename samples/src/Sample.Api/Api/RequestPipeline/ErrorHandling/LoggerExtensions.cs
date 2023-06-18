using System;
using Microsoft.Extensions.Logging;

namespace Sample.Api.Api.RequestPipeline.ErrorHandling;

public static partial class LoggerExtensions
{
    [LoggerMessage(
        Level = LogLevel.Error,
        Message = "Unhandled error occurred. Path={Path}")]
    public static partial void UnhandledErrorOccurred(
        this ILogger<UnhandledErrorHandlingMiddleware> logger, 
        string path, 
        Exception exception);
}
