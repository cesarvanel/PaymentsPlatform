using Shared.Domain.Event;

namespace Shared.Application.Cqrs.interfaces
{
    public interface IDomainEventStore
    {
        Task AppendAsync(IEnumerable<IAuditableEvent> events, CancellationToken ct = default);

    }
}
