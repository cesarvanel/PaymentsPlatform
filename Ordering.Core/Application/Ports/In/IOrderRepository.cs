

using Ordering.Core.Domain;

namespace Ordering.Core.Application.Ports.In
{
    public interface IOrderRepository
    {
        Task SaveAsync(Order order, CancellationToken ct = default);
        Task<Order?> GetByIdAsync(Guid id , CancellationToken ct = default);
    }
}
