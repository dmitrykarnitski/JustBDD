using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sample.Api.Api.RequestPipeline.ErrorHandling.Models;

namespace Sample.Api.Api.RequestPipeline.ErrorHandling;

public class UnhandledErrorHandlingMiddleware
{
    private readonly ILogger<UnhandledErrorHandlingMiddleware> _logger;

    private static readonly ErrorResponse genericResponse = new()
    {
        Message = ErrorMessages.GenericErrorMessage
    };

    public UnhandledErrorHandlingMiddleware(RequestDelegate _, ILogger<UnhandledErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>()!;

        _logger.UnhandledErrorOccurred(exceptionHandlerFeature.Path, exceptionHandlerFeature.Error);

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await context.Response.WriteAsJsonAsync(genericResponse);
    }
}
