using JustBDD.Core.Contexts.Stores;
using System;

namespace JustBDD.Core.Contexts;

public interface ITestContext : IDisposable
{
    ContextStore ContextStore { get; }
}
