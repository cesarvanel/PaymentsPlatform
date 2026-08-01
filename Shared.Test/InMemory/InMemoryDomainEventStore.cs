using Shared.Application.Cqrs.interfaces;
using Shared.Domain.Event;

namespace Shared.Test.InMemory
{
    public sealed class InMemoryDomainEventStore:IDomainEventStore
    {
        public List<IAuditableEvent> Events { get; } = [];

        public Task AppendAsync(IEnumerable<IAuditableEvent> events, CancellationToken ct = default)
        {
            Events.AddRange(events);
            return Task.CompletedTask;
        }
    }
}
