using Contracts.IntegrationEvents;
using Ordering.Core.Domain.Events;
using Shared.Application.Cqrs.interfaces;
using Shared.Domain.Event;


namespace Ordering.Core.Application.Subscribers
{
    public sealed class OrderCreatedSubscriber(IOutbox outbox) : IEventSubscriber
    {
        public bool IsSubscribedTo(IDomainEvent e) => e is OrderCreated;

        public async Task HandleAsync(IDomainEvent e, CancellationToken ct = default)
        {
            if (e is not OrderCreated created ) return;

            var integrationEvent = new OrderPlacedIntegrationEvent
                (
                OrderId: created.OrderId,
                Amount: created.TotalAmount,
                Currency: created.Currency,
                Items: [..created.Items.Select(item => new OrderPlacedItem(item.ProductId, item.Name, item.Quantity, item.UnitPrice, item.Currency))]);

               await outbox.AddAsync([integrationEvent], ct);
        }
    }
}
