using System;

namespace Sample.Api.Domain.Exceptions;

public class ItemNotFoundException : DomainException
{
    public Type ItemType { get; }

    public object ItemId { get; }

    public ItemNotFoundException(Type itemType, object itemId)
        : base("RequestedItemNotFound", $"{itemType.Name} with ID = '{itemId}' was not found.")
    {
        ItemType = itemType;
        ItemId = itemId;
    }
}
