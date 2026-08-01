using Billing.Core.Application.Subscribers;
using Billing.Core.Domain.Enum;
using Billing.Tests.Units.Builders;
using Billing.Tests.Units.InMemory;
using Contracts.IntegrationEvents;
using Shared.Domain.Vo;

namespace Billing.Tests.Units.Subcribers
{
    public class OrderPlacedSubscriberTest
    {
        private readonly InMemoryInvoiceRepository _inMemoryInvoiceRepository;
        private readonly OrderPlacedSubscriber _subscriber;

        public OrderPlacedSubscriberTest()
        {
            _inMemoryInvoiceRepository = new InMemoryInvoiceRepository();
            _subscriber = new OrderPlacedSubscriber(_inMemoryInvoiceRepository);
        }

        [Fact]
        public async Task OrderPlaced_CreateInvoice_WithMatchingData()
        {
            var orderId = Guid.NewGuid();
            var placedItems = new OrderPlacedItemBuilder().BuildMany(4);
            var domainEvent = new OrderPlacedIntegrationEvent(orderId, 1000.00m, Currency.Xaf, placedItems);


            await _subscriber.HandleAsync(domainEvent, TestContext.Current.CancellationToken);

            var invoice = Assert.Single(_inMemoryInvoiceRepository.GetAll());

            Assert.Equal(orderId, invoice.OrderId);
            Assert.Equal(1000.00m, invoice.TotalAmount.Value);
            Assert.Equal(Currency.Xaf, invoice.TotalAmount.Currency);
            Assert.Equal(4, invoice.InvoiceItems.Count);
            Assert.Equal(InvoiceState.Issued, invoice.State);


        }

    }
}
