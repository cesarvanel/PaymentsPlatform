using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Domain.Event
{
    public interface IEventSubscriber
    {
        bool IsSubscribedTo(IDomainEvent domainEvent);
        Task HandleAsync(IDomainEvent domainEvent, CancellationToken ct = default);

    }
}
