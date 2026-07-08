using Shared.Domain.Event;


namespace Shared.Application.Cqrs
{
    public interface IDomainEventDispatcher
    {
        Task PublishAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct = default);
    }
}
