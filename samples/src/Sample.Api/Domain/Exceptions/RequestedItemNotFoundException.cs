using System;

namespace Sample.Api.Domain.Exceptions;

public class RequestedItemNotFoundException : DomainException
{
    public Type ItemType { get; }

    public object ItemId { get; }

    public RequestedItemNotFoundException(Type itemType, object itemId)
        : base("RequestedItemNotFound", $"{itemType.Name} with ID = '{itemId}' was not found.")
    {
        ItemType = itemType;
        ItemId = itemId;
    }
}
