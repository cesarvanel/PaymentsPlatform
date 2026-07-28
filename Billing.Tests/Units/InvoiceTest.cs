using Billing.Core.Application.Subscribers;
using Billing.Tests.Units.Builders;
using Billing.Tests.Units.InMemory;
using Contracts.IntegrationEvents;
using Shared.Domain.Vo;

namespace Billing.Tests.Units
{
    public class InvoiceTest
    {
        [Fact]
        public async Task Handle_CreateInvoice_Successfully()
        {
            var invoiceRepository = new InMemoryInvoiceRepository();

            var handler = new OrderPlacedSubscriber(invoiceRepository);

            var placedItems = new OrderPlacedItemBuilder().BuildMany(4);
            var domainEvent = new OrderPlacedIntegrationEvent(Guid.NewGuid(), 1000.00m, Currency.Xaf, placedItems);

            await handler.HandleAsync(domainEvent, TestContext.Current.CancellationToken);

            Assert.Single(invoiceRepository.GetAll());

        }

    }
}
