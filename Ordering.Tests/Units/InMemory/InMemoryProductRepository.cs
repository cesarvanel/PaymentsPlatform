using Ordering.Core.Application.Ports.Out;
using Ordering.Core.Domain;


namespace Ordering.Tests.Units.InMemory
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly Dictionary<Guid , Product> _store = [];
        public Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return Task.FromResult(_store.TryGetValue(id, out var product) ? product : null);
        }

        public void Initialize(Product product)
        {
            _store[product.Snapshot.Id] = product;
        }
    }
}
