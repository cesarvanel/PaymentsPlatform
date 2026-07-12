using Shared.Domain.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Domain
{
    public abstract class AggregateRoot
    {

        public Guid Id { get; protected set; }


        private readonly List<IDomainEvent> _domainEvents = [];

        protected AggregateRoot(Guid id) => Id = id;
        protected AggregateRoot() { }

        protected void RaiseDomainEvent(IDomainEvent  domainEvent) => _domainEvents.Add(domainEvent);
        
        public void ClearDomainEvent() => _domainEvents.Clear();

        public List<IDomainEvent> DomainEvents => _domainEvents;
    }
}
