﻿using System;

namespace Sample.Api.Api.Exceptions;

public class ApiException : Exception
{
    public ApiException(string message)
        : base(message)
    {
    }
}
