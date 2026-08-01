using Shared.Application.Cqrs.interfaces;
using Shared.Domain.Event;

namespace Shared.Test.InMemory
{
    public sealed class InMemoryOutbox : IOutbox
    {
        public List<IIntegrationEvent> Events { get;} = [];
        public Task AddAsync(IEnumerable<IIntegrationEvent> events, CancellationToken ct = default)
        {
            Events.AddRange(events);
            return Task.CompletedTask;
        }

        public List<IIntegrationEvent> PendingEvents() => Events;
    }
}
