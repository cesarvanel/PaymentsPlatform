using Shared.Application.Cqrs.interfaces;
using Shared.Domain.Event;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
