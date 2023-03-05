using System;
using JustBDD.Core.Contexts.Stores;

namespace JustBDD.Core.Contexts;

public static class ContextStoreFactory
{
    private static Func<IContextStore> contextStoreFactory = () => new ContextStore();

    public static IContextStore Create()
    {
        return contextStoreFactory();
    }

    public static void SetContextStoreFactory(Func<IContextStore> factory)
    {
        contextStoreFactory = factory;
    }
}
