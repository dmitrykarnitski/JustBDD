using System.Diagnostics;

namespace Sample.Api.ComponentTests.Framework.Logging;

public class TestOutputTraceListener : TraceListener
{
    public override void Write(string? message)
    {
        TestOutputStreamHolder.Current?.WriteLine(message);
    }

    public override void WriteLine(string? message)
    {
        TestOutputStreamHolder.Current?.WriteLine(message);
    }
}
