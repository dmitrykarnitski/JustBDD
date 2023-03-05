using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Sample.Api.ComponentTests.Framework.Extensions;

namespace Sample.Api.ComponentTests.Framework.MockingSupport;

public class MockedInMemoryRepository<TItem, TId>
{
    private readonly Func<TItem, TId> _idSelector;
    private readonly Func<TId?, TId> _newIdGenerator;
    private readonly Action<TItem, TId> _idSetter;

    private readonly List<TItem> _items = new();

    public MockedInMemoryRepository(Func<TItem, TId> idSelector, Func<TId?, TId> newIdGenerator)
    {
        _idSelector = idSelector;
        _newIdGenerator = newIdGenerator;
        _idSetter = BuildIdSetter();
    }

    public void Initialize(IEnumerable<TItem> items)
    {
        _items.AddRange(items);
    }

    public IEnumerable<TItem> GetItems()
    {
        return _items.DeepClone();
    }

    protected IReadOnlyCollection<TItem> GetAllItems()
    {
        return _items.DeepClone();
    }

    public TItem? FindById(TId id)
    {
        return _items.FirstOrDefault(item => id!.Equals(_idSelector(item)));
    }

    public TItem Create(TItem itemToCreate)
    {
        var lastId = GetLastId();

        var newId = _newIdGenerator(lastId);

        _idSetter(itemToCreate, newId);

        _items.Add(itemToCreate);

        return itemToCreate;
    }

    public TItem Update(TItem itemToUpdate)
    {
        var (_, indexOfItemToUpdate) = _items
            .Select((item, index) => (item, index))
            .FirstOrDefault(tuple => tuple.item!.Equals(_idSelector(itemToUpdate)));

        _items.Insert(indexOfItemToUpdate, itemToUpdate);

        return itemToUpdate;
    }

    public void Delete(TId id)
    {
        var itemToDelete = _items.FirstOrDefault(item => id!.Equals(_idSelector(item)));
        if (itemToDelete != null)
        {
            _items.Remove(itemToDelete);
        }
    }

    private TId? GetLastId()
    {
        if (_items.Any())
        {
            return _idSelector(_items.Last());
        }

        return default;
    }

    private static Action<TItem, TId> BuildIdSetter()
    {
        var itemParameter = Expression.Parameter(typeof(TItem), "item");
        var idParameter = Expression.Parameter(typeof(TId), "id");

        var propertyInfo = typeof(TItem).GetProperty("Id")!;

        var itemIdProperty = Expression.Property(itemParameter, propertyInfo);

        var assign = Expression.Assign(itemIdProperty, idParameter);

        return Expression.Lambda<Action<TItem, TId>>(assign, itemParameter, idParameter).Compile();
    }
}
