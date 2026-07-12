using Shared.Domain.Event;


namespace Shared.Application.Cqrs.interfaces
{
    public interface IDomainEventDispatcher
    {
        Task PublishAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct = default);
    }
}
