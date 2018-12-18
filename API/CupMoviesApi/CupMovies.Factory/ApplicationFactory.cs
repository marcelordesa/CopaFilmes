using CupMovies.Domain.Contracts.Applications;

namespace CupMovies.Factory
{
    public class ApplicationFactory
    {
        public static IApplication GetApplicationInstance<T>() where T : IApplication, new()
        {
            return new T();
        }
    }
}