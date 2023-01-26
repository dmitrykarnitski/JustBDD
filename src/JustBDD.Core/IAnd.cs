namespace JustBDD.Core;

public interface IAnd<TStep> where TStep : StepBase<TStep>
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Is used for fluent syntax.")]
    TStep And { get; }
}
