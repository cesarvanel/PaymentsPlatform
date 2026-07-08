using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Domain.Event
{
    public interface IIntegrationEvent :IDomainEvent
    {
        Guid EventId { get; }

        string KeyRoot { get; }
    }
}
