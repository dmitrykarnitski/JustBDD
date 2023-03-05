namespace JustBDD.Core;

#pragma warning disable CA1716 // And is used for fluent syntax

public interface IAnd<out TStep> where TStep : StepBase<TStep>
{
    TStep And { get; }
}
