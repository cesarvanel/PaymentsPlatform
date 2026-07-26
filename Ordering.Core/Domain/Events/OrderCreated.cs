using Shared.Domain.Event;
using Shared.Domain.Vo;

namespace Ordering.Core.Domain.Events
{
    public record OrderCreated(Guid OrderId, decimal TotalAmount, Currency Currency, IEnumerable<OrderCreatedItem> Items) : IDomainEvent;

    public record OrderCreatedItem(Guid ProductId, string Name, int Quantity, decimal UnitPrice, Currency Currency);
}
