using Shared.Domain.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Domain
{
    public abstract class AggregateRoot
    {

        private readonly List<IDomainEvent> _domainEvents = [];

        protected AggregateRoot() { }

        protected void RaiseDomainEvent(IDomainEvent  domainEvent) => _domainEvents.Add(domainEvent);
        
        public void ClearDomainEvent() => _domainEvents.Clear();
    }
}
