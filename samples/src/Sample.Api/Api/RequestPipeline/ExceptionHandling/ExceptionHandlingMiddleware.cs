using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Sample.Api.Api.Exceptions;
using Sample.Api.Api.RequestPipeline.ExceptionHandling.Models;
using Sample.Api.Domain.Exceptions;

namespace Sample.Api.Api.RequestPipeline.ExceptionHandling;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>()!;

        int statusCode;
        ErrorResponse response;

        switch (exceptionHandlerPathFeature.Error)
        {
            case ValidationException validationException:
                response = BuildValidationErrorResponse(validationException);
                statusCode = StatusCodes.Status422UnprocessableEntity;
                break;
            case RequestedItemNotFoundException notFoundException:
                response = BuildNotFoundErrorResponse(notFoundException);
                statusCode = StatusCodes.Status404NotFound;
                break;
            default:
                response = BuildGenericErrorResponse();
                statusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(response);
    }

    private ErrorResponse BuildGenericErrorResponse()
    {
        return new ErrorResponse
        {
            Message = ErrorMessages.GenericErrorMessage
        };
    }

    private ErrorResponse BuildNotFoundErrorResponse(RequestedItemNotFoundException notFoundException)
    {
        return new NotFoundErrorResponse
        {
            Message = notFoundException.Message,
            ItemType = notFoundException.ItemType.Name,
            ItemId = notFoundException.ItemId.ToString()!
        };
    }

    private ErrorResponse BuildValidationErrorResponse(ValidationException validationException)
    {
        return new ValidationErrorResponse
        {
            Message = validationException.Message,
            Errors = validationException.Errors
        };
    }
}
