using Ordering.Core.Application.Ports.In;
using Ordering.Core.Domain;


namespace Ordering.Tests.Units.InMemory
{
    public class InMemoryOrderRepository:IOrderRepository
    {

        private readonly Dictionary<Guid, Order> _store = new();

        public Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return Task.FromResult(_store.TryGetValue(id, out var order) ?order:null);
        }

        public Task SaveAsync(Order order, CancellationToken ct = default)
        {
            _store[order.Id] = order;
            return Task.CompletedTask;
        }

        public void Initialize(Order order)
        {
            _store[order.Id] = order;
        }
    }
}
