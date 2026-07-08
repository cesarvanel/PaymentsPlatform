using Shared.Domain.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Application.Cqrs
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync(CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);
        Task CommitAsync(CancellationToken ct = default);
        Task RollbackAsync(CancellationToken ct = default);

        IReadOnlyList<IDomainEvent> CollectDomainEvents();
    }
}
