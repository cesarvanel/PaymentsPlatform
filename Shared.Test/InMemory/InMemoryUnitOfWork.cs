using Shared.Application.Cqrs.interfaces;
using Shared.Domain;
using Shared.Domain.Event;


namespace Shared.Test.InMemory
{
    public sealed class InMemoryUnitOfWork : IUnitOfWork
    {
        private readonly List<AggregateRoot> _tracked = [];

        public void Track(AggregateRoot aggregate) => _tracked.Add(aggregate);
        public Task BeginTransactionAsync(CancellationToken ct = default) => Task.CompletedTask;
        public Task CommitAsync(CancellationToken ct = default) => Task.CompletedTask;
        public Task RollbackAsync(CancellationToken ct = default) => Task.CompletedTask;
        public Task SaveChangesAsync(CancellationToken ct = default) => Task.CompletedTask;

        public IReadOnlyList<IDomainEvent> CollectDomainEvents()
        {
            var events = _tracked.SelectMany(t => t.DomainEvents).ToList();

            foreach (var e in _tracked) e.ClearDomainEvent();

            return events;
        }

    }

}
