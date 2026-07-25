using Shared.Domain.Event;

namespace Ordering.Core.Domain.Events
{
    public record OrderCreated(Guid OrderId, decimal TotalAmount) :IDomainEvent
    {
    }
}
