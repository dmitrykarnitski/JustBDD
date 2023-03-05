using System;
using System.Threading.Tasks;
using JustBDD.Core.Contexts.Stores;

namespace JustBDD.Core.Contexts;

public abstract class TestContextBase : ITestContext
{
    public IContextStore ContextStore { get; private set; } = null!;

    public virtual void Initialize(IContextStore contextStore)
    {
        ContextStore = contextStore;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await ContextStore.DisposeAsync();

        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        ContextStore.Dispose();
    }

    protected T? Get<T>(string key)
    {
        return ContextStore.Get<T>(key);
    }

    protected void Set<T>(string key, T? value)
    {
        ContextStore.Set(key, value);
    }

    protected T GetAndInitialiseIfNotSet<T>(string propertyName)
        where T : new()
    {
        if (!ContextStore.Contains(propertyName))
        {
            Set(propertyName, new T());
        }

        return Get<T>(propertyName)!;
    }

    protected T GetAndInitialiseIfNotSet<T>(
        string propertyName,
        Func<T> factory)
    {
        if (!ContextStore.Contains(propertyName))
        {
            Set(propertyName, factory());
        }

        return Get<T>(propertyName)!;
    }
}
