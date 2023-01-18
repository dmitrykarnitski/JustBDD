using JustBDD.Core.Contexts.Stores;

namespace JustBDD.Core.Contexts;

public abstract class SuiteBase : ITestContext
{
    public ContextStore ContextStore => SuiteStore.Instance;

    public void Dispose()
    {
        SuiteStore.Instance?.Dispose();
    }
}
