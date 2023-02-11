using System;

namespace Sample.Api.Domain.Exceptions;

public class DomainException : Exception
{
    public string ErrorCode { get; }

    public DomainException(string code, string message)
        : base(message)
    {
        ErrorCode = code;
    }
}
