namespace SiteGenerator;

public interface IProcessingStep<T, TContext>
{
    T Process(T input, ref TContext context);
}
