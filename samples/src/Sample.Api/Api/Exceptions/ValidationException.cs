using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sample.Api.Api.Exceptions;

public class ValidationException : ApiException
{
    public ValidationException(IDictionary<string, string[]> errors)
        : base("Request validation failed.")
    {
        Errors = new ReadOnlyDictionary<string, string[]>(errors);
    }

    public IReadOnlyDictionary<string, string[]> Errors { get; }
}
