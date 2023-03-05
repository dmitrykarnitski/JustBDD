namespace JustBDD.Core.Contexts.Stores;

public static class SuiteStore
{
    public static readonly IContextStore Instance = ContextStoreFactory.Create();
}
