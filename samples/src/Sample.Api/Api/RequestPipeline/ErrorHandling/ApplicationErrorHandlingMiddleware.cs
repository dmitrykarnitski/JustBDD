using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sample.Api.Api.Exceptions;
using Sample.Api.Api.RequestPipeline.ErrorHandling.Models;
using Sample.Api.Domain.Exceptions;

namespace Sample.Api.Api.RequestPipeline.ErrorHandling;

public class ApplicationErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ApplicationErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException validationException)
        {
            await SetErrorResponseAsync(
                context,
                StatusCodes.Status422UnprocessableEntity,
                BuildValidationErrorResponse(validationException));
        }
        catch (ItemNotFoundException notFoundException)
        {
            await SetErrorResponseAsync(
                context,
                StatusCodes.Status404NotFound,
                BuildNotFoundErrorResponse(notFoundException));
        }
    }

    private ItemNotFoundErrorResponse BuildNotFoundErrorResponse(ItemNotFoundException notFoundException)
    {
        return new ItemNotFoundErrorResponse
        {
            Message = ErrorMessages.ItemNotFoundErrorMessage,
            ItemType = notFoundException.ItemType.Name,
            ItemId = notFoundException.ItemId.ToString()!
        };
    }

    private ValidationErrorResponse BuildValidationErrorResponse(ValidationException validationException)
    {
        return new ValidationErrorResponse
        {
            Message = ErrorMessages.ValidationErrorMessage,
            Errors = validationException.Errors
        };
    }

    private async Task SetErrorResponseAsync<T>(HttpContext httpContext, int statusCode, T response)
    {
        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(response);
    }
}
