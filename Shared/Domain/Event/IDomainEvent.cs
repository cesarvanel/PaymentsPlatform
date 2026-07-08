
namespace Shared.Domain.Event
{
    public interface IDomainEvent
    {
        DateTime OccuredOn => DateTime.UtcNow;
    }
}
