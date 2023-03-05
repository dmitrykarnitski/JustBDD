using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace JustBDD.Core.Contexts.Stores;

internal class ContextStore : IContextStore
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

    public async ValueTask DisposeAsync()
    {
        foreach (var item in _contextStore)
        {
            if (item.Value is IAsyncDisposable disposable)
            {
                try
                {
                    await disposable.DisposeAsync();
                }
#pragma warning disable CA1031
                catch (Exception e)
#pragma warning restore CA1031
                {
                    Trace.WriteLine($"Disposing failed for item={item.Key} of type={item.Value.GetType().Name} with error={e.Message}.");
                }
            }
        }

        Dispose();
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
