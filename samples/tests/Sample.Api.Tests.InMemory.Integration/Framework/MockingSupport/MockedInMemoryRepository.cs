using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Sample.Api.Tests.InMemory.Integration.Framework.Extensions;

namespace Sample.Api.Tests.InMemory.Integration.Framework.MockingSupport;

public class MockedInMemoryRepository<TItem, TId>
{
    private static readonly Func<TItem, TId, bool> idsComparer = BuildIdsComparer();
    private static readonly Func<TItem, TId> idSelector = BuildIdSelector();
    private static readonly Action<TItem, TId> idSetter = BuildIdSetter();
    private static readonly Func<TItem, TId, TId> newIdGenerator = BuildNewIdGenerator();

    private readonly List<TItem> _items = new();

    public void Initialize(IEnumerable<TItem> items)
    {
        _items.AddRange(items);
    }
    public IEnumerable<TItem> GetItems()
    {
        return _items.DeepClone()!;
    }

    protected IReadOnlyCollection<TItem> GetAllItems()
    {
        return _items.DeepClone()!;
    }

    public TItem? FindById(TId id)
    {
        return _items.FirstOrDefault(item => idsComparer(item, id));
    }

    public TItem Create(TItem itemToCreate)
    {
        var highestId = _items.Select(item => idSelector(item)).OrderByDescending(id => id).First();

        var newId = newIdGenerator(itemToCreate, highestId);

        idSetter(itemToCreate, newId);

        _items.Add(itemToCreate);

        return itemToCreate;
    }

    public TItem Update(TItem item)
    {
        var (_, index) = _items.Select((c, i) => (c, i)).FirstOrDefault(t => idsComparer(t.c, idSelector(item)));

        _items.Insert(index, item);

        return item;
    }

    public void Delete(TId id)
    {
        var item = _items.FirstOrDefault(c => idsComparer(c, id));
        if (item != null)
        {
            _items.Remove(item);
        }
    }

    private static Func<TItem, TId, bool> BuildIdsComparer()
    {
        var itemParameter = Expression.Parameter(typeof(TItem), "item");
        var idParameter = Expression.Parameter(typeof(TId), "id");

        var propertyInfo = typeof(TItem).GetProperty("Id");
        if (propertyInfo?.PropertyType != typeof(TId))
        {
            throw new InvalidOperationException("Type of ID property from item does not match with repository ID property type");
        }

        var getItemId = Expression.Property(itemParameter, propertyInfo);

        var compareIds = Expression.Equal(getItemId, idParameter);

        return Expression.Lambda<Func<TItem, TId, bool>>(compareIds, itemParameter, idParameter).Compile();
    }

    private static Func<TItem, TId> BuildIdSelector()
    {
        var itemParameter = Expression.Parameter(typeof(TItem), "item");

        var propertyInfo = typeof(TItem).GetProperty("Id");
        if (propertyInfo?.PropertyType != typeof(TId))
        {
            throw new InvalidOperationException("Type of ID property from item does not match with repository ID property type");
        }

        var getItemId = Expression.Property(itemParameter, propertyInfo);

        return Expression.Lambda<Func<TItem, TId>>(getItemId, itemParameter).Compile();
    }

    private static Action<TItem, TId> BuildIdSetter()
    {
        var itemParameter = Expression.Parameter(typeof(TItem), "item");
        var idParameter = Expression.Parameter(typeof(TId), "id");

        var propertyInfo = typeof(TItem).GetProperty("Id");
        if (propertyInfo?.PropertyType != typeof(TId))
        {
            throw new InvalidOperationException("Type of ID property from item does not match with repository ID property type");
        }

        var itemIdProperty = Expression.Property(itemParameter, propertyInfo);

        var assign = Expression.Assign(itemIdProperty, idParameter);

        return Expression.Lambda<Action<TItem, TId>>(assign, itemParameter, idParameter).Compile();
    }

    private static Func<TItem, TId, TId> BuildNewIdGenerator()
    {
        var itemParameter = Expression.Parameter(typeof(TItem), "item");
        var idParameter = Expression.Parameter(typeof(TId), "id");

        var propertyInfo = typeof(TItem).GetProperty("Id");
        if (propertyInfo?.PropertyType != typeof(TId))
        {
            throw new InvalidOperationException("Type of ID property from item does not match with repository ID property type");
        }

        if (typeof(TId) == typeof(int))
        {
            return Expression.Lambda<Func<TItem, TId, TId>>(Expression.Increment(idParameter), itemParameter, idParameter).Compile();
        }

        if (typeof(TId) == typeof(string))
        {
            var newGuidMethod = typeof(Guid).GetMethod(nameof(Guid.NewGuid))!;
            var toStringMethod = typeof(Guid).GetMethod(nameof(Guid.ToString))!;
            var newGuidMethodCall = Expression.Call(newGuidMethod);

            var toStringMethodCall = Expression.Call(newGuidMethodCall, toStringMethod);

            return Expression.Lambda<Func<TItem, TId, TId>>(toStringMethodCall, itemParameter, idParameter).Compile();
        }

        throw new InvalidOperationException("Id type is not supported");
    }
}
