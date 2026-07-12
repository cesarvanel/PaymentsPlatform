

namespace Shared.Application.providers
{
    public interface IServiceResolver
    {
        T Resolve<T>() where T : notnull;

    }
}
