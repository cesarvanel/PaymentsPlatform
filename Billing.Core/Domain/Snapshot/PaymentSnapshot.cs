using Billing.Core.Domain.Enum;
using Shared.Domain.Vo;

namespace Billing.Core.Domain.Snapshot
{
    public record PaymentSnapshot(
        Guid PaymentId,
        Guid InvoiceId,
        decimal Amount,
        Currency Currency,
        PaymentStatus Status,
        PaymentMethod Method,
        DateTime PaidAt);
}
