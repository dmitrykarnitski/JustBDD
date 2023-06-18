namespace Sample.Api.Api.RequestPipeline.ErrorHandling.Models;

public class ItemNotFoundErrorResponse : ErrorResponse
{
    public string ItemType { get; init; } = null!;

    public string ItemId { get; init; } = null!;
}
