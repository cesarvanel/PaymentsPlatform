using Shared.Application.Messaging;


namespace Shared.Application.Cqrs.interfaces
{

    public interface ICommand<TResponse>;
    public interface ICommandHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken ct = default);
    }

}
