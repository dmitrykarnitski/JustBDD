using System;
using System.Collections.Generic;

namespace JustBDD.Core.Contexts.Stores;

public class ContextStore : IDisposable
{
    private readonly IDictionary<string, object?> _contextStore;

    public ContextStore()
    {
        _contextStore = new Dictionary<string, object?>();
    }

    public T? Get<T>(string propertyName)
    {
        return _contextStore.TryGetValue(propertyName, out var value)
            ? (T?)value
            : default;
    }

    public void Set(string propertyName, object? value)
    {
        _contextStore[propertyName] = value;
    }

    public bool Contains(string propertyName)
    {
        return _contextStore.ContainsKey(propertyName);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        foreach (var item in _contextStore)
        {
            if (item.Value is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
