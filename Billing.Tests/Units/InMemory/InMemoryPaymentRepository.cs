

using Billing.Core.Application.Ports.Out;
using Billing.Core.Domain;

namespace Billing.Tests.Units.InMemory
{
    public class InMemoryPaymentRepository : IPaymentRepository
    {
        private readonly Dictionary<Guid, Payment> _stores = [];
        public Task<Payment?> GetByIdAsync(Guid paymentId, CancellationToken ct = default)
        {
            return Task.FromResult(_stores.TryGetValue(paymentId, out var payment) ? payment : null);
        }

        public Task SaveAsync(Payment payment, CancellationToken ct = default)
        {
            _stores[payment.Id] = payment;
            return Task.CompletedTask;
        }

        public Task SaveManyAsync(IReadOnlyList<Payment> payments, CancellationToken ct = default)
        {
            foreach (var payment in payments)
            {
                _stores[payment.Id] = payment;
            }

            return Task.CompletedTask;
        }
    }
}
