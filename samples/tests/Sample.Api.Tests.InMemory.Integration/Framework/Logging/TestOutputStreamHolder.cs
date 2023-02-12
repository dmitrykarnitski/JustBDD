using System.IO;

namespace Sample.Api.Tests.InMemory.Integration.Framework.Logging;

/// <summary>
/// This class helps to share TestContext.Out writer between test and application logs. Without it - application logs will not be written to the test output.
/// </summary>
public static class TestOutputStreamHolder
{
    public static TextWriter? Current { get; set; }
}
