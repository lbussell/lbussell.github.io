namespace SiteGenerator;

public interface IPipeline<T, TContext>
{
    public IPipeline<T, TContext> Add(IProcessingStep<T, TContext> step);

    public void GenerateAsync(T input);
}
