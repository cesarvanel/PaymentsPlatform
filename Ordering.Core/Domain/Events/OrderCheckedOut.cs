using Shared.Domain.Event;


namespace Ordering.Core.Domain.Events
{
    public record OrderCheckedOut(Guid OrderId, decimal Total) : IAuditableEvent;

}
