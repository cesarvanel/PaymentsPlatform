using Billing.Core.Application.Ports.Out;
using Billing.Core.Domain;
using Billing.Infra.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Billing.Infra.Persistence.Adapters
{
    public class EfInvoiceRepository(BillingDbContext db) : IInvoiceRepository
    {
        public async Task<Invoice?> GetByIdAsync(Guid invoiceId, CancellationToken ct = default)
        {
            var model = await db.Invoices.Include(i => i.Payments).FirstOrDefaultAsync(i =>  i.Id == invoiceId, ct);

            return model is null ? null : InvoiceModel.ToDomain(model);
        }

        public async Task SaveAsync(Invoice invoice, CancellationToken ct = default)
        {
            var model = InvoiceModel.ToPersistence(invoice.ToSnapshot());
            await db.Invoices.AddAsync(model, ct);
            await db.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Invoice invoice, CancellationToken ct = default)
        {
            var model = InvoiceModel.ToPersistence(invoice.ToSnapshot());
            db.Invoices.Update(model);
            await db.SaveChangesAsync(ct);
        }
    }
}
