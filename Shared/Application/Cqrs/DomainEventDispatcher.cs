using Shared.Domain.Event;
using System.Collections;


namespace Shared.Application.Cqrs
{
    public sealed class DomainEventDispatcher(IEnumerable<IEventSubscriber> subscribers) : IDomainEventDispatcher
    {
        public async Task PublishAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct)
        {

            foreach (var domainEvent in domainEvents)
            {

                foreach (var subscriber in subscribers)
                {
                    if (subscriber.IsSubscribedTo(domainEvent))
                    {
                        await subscriber.HandleAsync(domainEvent, ct);
                    }
                }
            }
        }
    }
}
