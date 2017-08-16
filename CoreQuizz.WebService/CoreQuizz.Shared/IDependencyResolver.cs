namespace CoreQuizz.Shared
{
    public interface IDependencyResolver
    {
        TRequest Resolve<TRequest>();
    }
}
