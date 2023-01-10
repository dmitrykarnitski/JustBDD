using JustBDD.Core.Contexts.Stores;

namespace JustBDD.Core.Contexts;

public abstract class TestContextBase : ITestContext
{
    public ContextStore ContextStore { get; private set; }

    public virtual void Initialize(ContextStore contextStore)
    {
        ContextStore = contextStore;
    }

    public void Dispose()
    {
        ContextStore?.Dispose();
    }
}
