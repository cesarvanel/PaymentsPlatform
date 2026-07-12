using Shared.Application.Cqrs.interfaces;
using Shared.Application.Messaging;
using Shared.Application.providers;

namespace Shared.Application.Cqrs.abstracts
{
    public abstract class CommandHandlerWrapper<TResponse>
    {
        public abstract Task<Result<TResponse>> HandleAsync(
            ICommand<TResponse> command, IServiceResolver provider, CancellationToken ct);
    }

    public sealed class CommandHandlerWrapper<TCommand, TResponse>
    : CommandHandlerWrapper<TResponse>
    where TCommand : ICommand<TResponse>
    {
        public override async Task<Result<TResponse>> HandleAsync(
            ICommand<TResponse> command, IServiceResolver resolver, CancellationToken ct)
        {
            var handler = resolver.Resolve<ICommandHandler<TCommand, TResponse>>();
            return await handler.HandleAsync((TCommand)command, ct);
        }
    }
}
