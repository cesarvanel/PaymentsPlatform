
using Shared.Domain.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Application.Cqrs
{
    public interface  IOutbox
    {
        Task AddAsync(IEnumerable<IIntegrationEvent> events, CancellationToken ct = default);
    }
}
