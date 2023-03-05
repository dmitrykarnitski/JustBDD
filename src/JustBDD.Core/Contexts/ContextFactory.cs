using JustBDD.Core.Contexts.Stores;

namespace JustBDD.Core.Contexts;

public static class ContextFactory
{
    public static TContext Create<TContext>(IContextStore contextStore)
        where TContext : TestContextBase, new()
    {
        var context = new TContext();
        context.Initialize(contextStore);

        return context;
    }
}
