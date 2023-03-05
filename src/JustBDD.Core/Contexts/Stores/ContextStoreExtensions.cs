using System;

namespace JustBDD.Core.Contexts.Stores;

public static class ContextStoreExtensions
{
    public static T GetAndInitialiseIfNotSet<T>(
        this IContextStore contextStore,
        string propertyName)
        where T : new()
    {
        if (!contextStore.Contains(propertyName))
        {
            contextStore.Set(propertyName, new T());
        }

        return contextStore.Get<T>(propertyName)!;
    }

    public static T GetAndInitialiseIfNotSet<T>(
        this IContextStore contextStore,
        string propertyName,
        Func<T> factory)
    {
        if (!contextStore.Contains(propertyName))
        {
            contextStore.Set(propertyName, factory());
        }

        return contextStore.Get<T>(propertyName)!;
    }
}
