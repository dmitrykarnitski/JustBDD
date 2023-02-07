using System;
using JustBDD.Core.Contexts.Stores;

namespace JustBDD.Core.Contexts;

public interface ITestContext : IDisposable
{
    ContextStore ContextStore { get; }
}
