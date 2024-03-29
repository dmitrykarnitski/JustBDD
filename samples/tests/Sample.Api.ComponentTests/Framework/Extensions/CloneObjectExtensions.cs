﻿using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Sample.Api.ComponentTests.Framework.Extensions;

public static class CloneObjectExtensions
{
    [return: NotNullIfNotNull(nameof(obj))]
    public static T? DeepClone<T>(this T? obj)
    {
        return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
    }
}
