using System;

namespace JustBDD.Core.Contexts.Stores;

#pragma warning disable CA1716

public interface IContextStore : IAsyncDisposable, IDisposable
{
    T? Get<T>(string propertyName);

    void Set(string propertyName, object? value);

    bool Contains(string propertyName);
}
