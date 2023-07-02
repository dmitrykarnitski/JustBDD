using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Sample.Api.ComponentTests.Framework.Logging;

public class LogEntry
{
    private readonly string _formattedMessage;
    private readonly DateTime _timestamp;
    private readonly int _indent;

    public LogLevel Level { get; }
    public string? OriginalFormat { get; }
    public Exception? Exception { get; }
    public Dictionary<string, object?> MessageState { get; }
    public Dictionary<string, object?> Scope { get; }

    public LogEntry(
        LogLevel logLevel,
        string formattedMessage,
        object? messageState,
        object? scope,
        Exception? exception,
        DateTime timestamp,
        int indent)
    {
        Level = logLevel;

        var messageStateEnumerable = messageState as IEnumerable<KeyValuePair<string, object?>>
                                     ?? Enumerable.Empty<KeyValuePair<string, object?>>();

        MessageState = new Dictionary<string, object?>(messageStateEnumerable);
        if (!MessageState.Any() && messageState is not null)
        {
            MessageState["Default"] = messageState;
        }

        if (MessageState.TryGetValue("{OriginalFormat}", out var originalFormat))
        {
            OriginalFormat = originalFormat as string;
            MessageState.Remove("{OriginalFormat}");
        }

        var scopeEnumerable = scope as IEnumerable<KeyValuePair<string, object?>>
                              ?? Enumerable.Empty<KeyValuePair<string, object?>>();

        Scope = new Dictionary<string, object?>(scopeEnumerable);
        if (!Scope.Any() && scope is not null)
        {
            Scope["Default"] = scope;
        }

        Exception = exception;

        _formattedMessage = formattedMessage;
        _timestamp = timestamp;
        _indent = indent;
    }

    public void WriteTo(TextWriter output)
    {
        WriteMessage(output, _indent);
        WriteStateAndScope(output, _indent + 4);

        if (Exception is null)
        {
            return;
        }

        WriteException(output, Exception, _indent + 4);

        if (Exception.InnerException is not null)
        {
            WriteInnerException(output, Exception, _indent + 8);
        }
    }

    private void WriteMessage(TextWriter output, int indent)
    {
        var message = OriginalFormat ?? _formattedMessage.ReplaceLineEndings("  ");

        Write(output, $"[{_timestamp:O} - {Level}] {message}", indent, '·');
    }

    private void WriteStateAndScope(TextWriter output, int indent)
    {
        foreach (var entry in MessageState.Concat(Scope).OrderBy(x => x.Key))
        {
            var entryValue = entry.Value?.ToString() ?? "(empty)";
            Write(output, $"{entry.Key}: {entryValue.ReplaceLineEndings("  ")}", indent);
        }

        if (OriginalFormat is not null)
        {
            Write(output, $"FormattedMessage: {_formattedMessage.ReplaceLineEndings("  ")}", indent);
        }
    }

    private void WriteException(TextWriter output, Exception exception, int indent)
    {
        Write(output, $"Exception: {exception.Message}", indent);
        Write(output, $"Stack Trace: {exception.StackTrace}", indent + 2);
    }

    private void WriteInnerException(TextWriter output, Exception exception, int indent)
    {
        WriteException(output, exception, indent);

        if (exception.InnerException is not null)
        {
            WriteInnerException(output, exception.InnerException, indent + 4);
        }
    }

    private void Write(TextWriter output, string text, int indent, char indentChar = ' ')
    {
        output.WriteLine(IndentLineEndings($"{new string(indentChar, indent)}{text}", indent));
    }

    private string IndentLineEndings(string text, int indent)
    {
        return text.ReplaceLineEndings($"{Environment.NewLine}{new string(' ', indent)}");
    }
}
