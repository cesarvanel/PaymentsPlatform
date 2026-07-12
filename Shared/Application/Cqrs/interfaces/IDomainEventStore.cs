using Shared.Domain.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Application.Cqrs.interfaces
{
    public interface IDomainEventStore
    {
        Task AppendAsync(IEnumerable<IAuditableEvent> events, CancellationToken ct = default);

    }
}
