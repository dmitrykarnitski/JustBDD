using System;

namespace Sample.Api.Framework.Services;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
