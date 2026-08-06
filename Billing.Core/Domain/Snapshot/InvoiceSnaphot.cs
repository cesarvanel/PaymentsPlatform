using Billing.Core.Domain.Enum;
using Shared.Domain.Vo;

namespace Billing.Core.Domain.Snapshot
{
    public record InvoiceSnapshot(
        Guid InvoiceId, 
        Guid OrderId, 
        decimal TotalAmount, 
        Currency Currency, 
        InvoiceState State,
        IReadOnlyList<InvoiceItemSnapshot> InvoiceItemSnapshot,
        IReadOnlyList<PaymentSnapshot> PaymentsSnapshot);

    public record InvoiceItemSnapshot(
        decimal Amount,
        Currency Currency,
        Guid ProductId,
        string ProductName,
        int Quantity);
}
