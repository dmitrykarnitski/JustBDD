using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using JustBDD.Core;
using Sample.Api.Api.RequestPipeline.ErrorHandling.Models;
using Sample.Api.Api.RequestPipeline.ErrorHandling;
using Sample.Api.ComponentTests.Framework.Steps.Base;

namespace Sample.Api.ComponentTests.Framework.Steps.Then.TheCall.WillFailWith;

public class WillFailWithStep : Step<ThenStep>
{
    private static readonly ErrorResponse internalServerErrorResponse = new()
    {
        Message = ErrorMessages.GenericErrorMessage
    };

    public IAnd<ThenStep> AuthenticationError()
    {
        return StatusCodeShouldBe(HttpStatusCode.Unauthorized);
    }

    public IAnd<ThenStep> ValidationError(string errorMessage, string propertyName)
    {
        return StatusCodeShouldBe(HttpStatusCode.UnprocessableEntity)
            .And.TheCall.WillHaveAResponse.EqualTo(BuildValidationErrorResponse(errorMessage, propertyName));
    }

    public IAnd<ThenStep> InternalServerError()
    {
        return StatusCodeShouldBe(HttpStatusCode.InternalServerError)
            .And.TheCall.WillHaveAResponse.EqualTo(internalServerErrorResponse);
    }

    private IAnd<ThenStep> StatusCodeShouldBe(HttpStatusCode statusCode)
    {
        Scenario.HttpResponse.Should().NotBeNull();
        Scenario.HttpResponse!.StatusCode.Should().Be(statusCode);

        return this;
    }

    private ValidationErrorResponse BuildValidationErrorResponse(string errorMessage, string propertyName)
    {
        return new ValidationErrorResponse
        {
            Message = ErrorMessages.ValidationErrorMessage,
            Errors = new Dictionary<string, string[]>
            {
                { propertyName, new[] { errorMessage.Replace("{PropertyName}", propertyName) } }
            }
        };
    }
}
