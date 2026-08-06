using Shared.Domain.Event;

namespace Shared.Application.Cqrs.interfaces
{
    public interface  IOutbox
    {
        Task AddAsync(IEnumerable<IIntegrationEvent> events, CancellationToken ct = default);
    }
}
