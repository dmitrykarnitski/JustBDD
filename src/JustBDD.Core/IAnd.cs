namespace JustBDD.Core;

public interface IAnd<TStep> where TStep : StepBase<TStep>
{
    TStep And { get; }
}
