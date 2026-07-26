using Billing.Core.Application.Ports.Out;
using Billing.Core.Domain;
using Billing.Core.Domain.Vo;
using Contracts.IntegrationEvents;
using Shared.Domain.Event;
using Shared.Domain.Vo;

namespace Billing.Core.Application.Subscribers
{
    public sealed class OrderPlacedSubscriber(IInvoiceRepository InvoiceRepository) : IEventSubscriber
    {
        public bool IsSubscribedTo(IDomainEvent e) => e is OrderPlacedIntegrationEvent;


        public async Task HandleAsync(IDomainEvent e, CancellationToken ct = default)
        {
            if (e is not OrderPlacedIntegrationEvent placed) return;

            var invoiceItems = placed.Items.Select(item => new InvoiceItem(item.ProductId, item.Name, new Quantity(item.Quantity), new Money(item.UnitPrice, placed.Currency))).ToList();

            var invoice = Invoice.Create(Guid.NewGuid(), placed.OrderId, invoiceItems, placed.Amount, placed.Currency);

            await InvoiceRepository.SaveAsync(invoice, ct);

        }

       
    }
}
