namespace Sample.Api.Api.RequestPipeline.ExceptionHandling.Models;

public class NotFoundErrorResponse : ErrorResponse
{
    public string ItemType { get; init; } = null!;

    public string ItemId { get; init; } = null!;
}
