using Shared.Domain.Vo;

namespace Billing.Core.Domain.Vo
{
    public record InvoiceItem(Guid ProductId, string Name, Quantity Quantity, Money Price)
    {

    }
}
