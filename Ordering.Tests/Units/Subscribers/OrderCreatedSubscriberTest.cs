using Contracts.IntegrationEvents;
using Ordering.Core.Application.Subscribers;
using Ordering.Core.Domain.Events;
using Shared.Domain.Vo;
using Shared.Test.InMemory;

namespace Ordering.Tests.Units.Subscribers
{
    public class OrderCreatedSubscriberTest
    {
        [Fact]
        public async Task OrderCreated_ProducesIntegrationEvent_InOutbox()
        {
            var outbox = new InMemoryOutbox();
            var subcriber = new OrderCreatedSubscriber(outbox);
            var orderId = Guid.NewGuid();

            var items = new List<OrderCreatedItem>
            {
                new(Guid.NewGuid(), "Riz", 2, 15_000m, Currency.Xaf),
                new(Guid.NewGuid(), "Huile", 1, 40_000m, Currency.Xaf)
            };
            var domainEvent = new OrderCreated(orderId, 1000.00m, Currency.Xaf, items);

            await subcriber.HandleAsync(domainEvent, TestContext.Current.CancellationToken);

            var integratedEvent = Assert.IsType<OrderPlacedIntegrationEvent>(Assert.Single(outbox.PendingEvents()));

            Assert.Equal(orderId, integratedEvent.OrderId);
            Assert.Equal(domainEvent.TotalAmount, integratedEvent.Amount);
            Assert.Equal(domainEvent.Currency, integratedEvent.Currency);
            Assert.Equal(domainEvent.Items.Count(), integratedEvent.Items.Count);
            Assert.Equal("Riz", integratedEvent.Items[0].Name);
            Assert.Equal(2, integratedEvent.Items[0].Quantity);
        }
    }
}
