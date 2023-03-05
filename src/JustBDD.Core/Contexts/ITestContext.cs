using System;
using JustBDD.Core.Contexts.Stores;

namespace JustBDD.Core.Contexts;

public interface ITestContext : IAsyncDisposable, IDisposable
{
    IContextStore ContextStore { get; }
}
