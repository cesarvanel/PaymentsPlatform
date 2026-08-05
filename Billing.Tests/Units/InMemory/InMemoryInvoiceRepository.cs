using Billing.Core.Application.Ports.Out;
using Billing.Core.Domain;
using Billing.Core.Domain.Enum;

namespace Billing.Tests.Units.InMemory
{
    public class InMemoryInvoiceRepository : IInvoiceRepository
    {
        private readonly Dictionary<Guid, Invoice> _stores = [];
        public Task<Invoice?> GetByIdAsync(Guid invoiceId, CancellationToken ct = default)
        {
            return Task.FromResult(_stores.TryGetValue(invoiceId, out var invoice)? invoice : null);
        }

        public  Task SaveAsync(Invoice invoice, CancellationToken ct = default)
        {
            _stores[invoice.Id] = invoice;
            return Task.CompletedTask;
        }

        public List<Invoice> GetAll() => _stores.Values.ToList();

        public Task UpdateAsync(Invoice invoice, CancellationToken ct = default)
        {
            _stores[invoice.Id] = invoice;
            return Task.CompletedTask;
        }

        public void Initialize(Guid invoiceId, decimal amount = 1000.00m)
        {
            var invoice = Invoice.Create(invoiceId, Guid.NewGuid(), [], amount, Shared.Domain.Vo.Currency.Xaf);

            SaveAsync(invoice, default);
        }

        public void SeedWithPayment(Guid invoiceId, List<Payment> payments)
        {
            var totalAnmount = payments.Aggregate(0.0m, (total, payment) => total += payment.Amount.Value);
            var invoice = Invoice.Reconstitue(invoiceId, Guid.NewGuid(), [], totalAnmount, Shared.Domain.Vo.Currency.Xaf, payments,InvoiceState.Paid);

            SaveAsync(invoice, default);

        }
    }
}
