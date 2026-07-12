using Shared.Application.Cqrs.interfaces;
using Shared.Domain.Event;


namespace Shared.Test.InMemory
{
    public sealed class InMemoryUnitOfWork : IUnitOfWork
    {
        public Task BeginTransactionAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IDomainEvent> CollectDomainEvents()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task RollbackAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }

}
