using Shared.Application.Messaging;


namespace Shared.Application.Cqrs.interfaces
{
    public interface ICommandDispatcher
    {
        Task<Result<TResponse>> ExecuteAsync<TResponse>(ICommand<TResponse> command, CancellationToken ct = default);
    }
}
