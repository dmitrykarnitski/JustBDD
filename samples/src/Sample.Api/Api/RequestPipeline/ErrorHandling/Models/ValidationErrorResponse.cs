using System.Collections.Generic;

namespace Sample.Api.Api.RequestPipeline.ErrorHandling.Models;

public class ValidationErrorResponse : ErrorResponse
{
    public IReadOnlyDictionary<string, string[]> Errors { get; init; } = null!;
}
