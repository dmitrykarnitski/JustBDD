using System.Threading.Tasks;
using JustBDD.Core.Contexts.Stores;

namespace JustBDD.Core.Contexts;

public abstract class SuiteBase : ITestContext
{
    public IContextStore ContextStore => SuiteStore.Instance;

    public void Dispose()
    {
        Dispose(true);
    }

    public async ValueTask DisposeAsync()
    {
        await SuiteStore.Instance.DisposeAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        SuiteStore.Instance.Dispose();
    }
}
