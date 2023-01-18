namespace JustBDD.Core.Contexts.Stores;

public static class ContextStoreExtensions
{
    public static T GetAndInitialiseIfNotSet<T>(this ContextStore contextStore, string propertyName) where T : new()
    {
        if (!contextStore.Contains(propertyName))
        {
            contextStore.Set(propertyName, new T());
        }

        return contextStore.Get<T>(propertyName)!;
    }
}
