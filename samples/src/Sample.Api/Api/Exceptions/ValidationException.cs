using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sample.Api.Api.Exceptions;

public class ValidationException : ApiException
{
    public ValidationException(IDictionary<string, string[]> errors)
        : base("A validation error has occurred.")
    {
        Errors = new ReadOnlyDictionary<string, string[]>(errors);
    }

    public IReadOnlyDictionary<string, string[]> Errors { get; }
}
