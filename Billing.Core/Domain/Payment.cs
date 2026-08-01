using Billing.Core.Domain.Enum;
using Billing.Core.Domain.Exceptions;
using Shared.Domain.Vo;

namespace Billing.Core.Domain
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public Guid InvoiceId { get; private set; }
        public Money Amount { get; private set; }
        public PaymentMethod Method { get; private set; }

        public DateTime PaidAt { get; private set; }

        public PaymentStatus Status { get; private set; }


        private Payment(Guid id, Guid invoiceId, PaymentStatus status, decimal amount, Currency currency, PaymentMethod method, DateTime paidAt)
        {
            Amount = new Money(amount, currency);
            Method = method;
            PaidAt = paidAt;
            Id = id;
            InvoiceId = invoiceId;
            Status = status;
        }

        public static Payment Create(Guid id, Guid invoiceId, decimal amount, Currency currency, PaymentMethod method, DateTime paidAt)
        {
            return new Payment(id, invoiceId, PaymentStatus.Active, amount, currency, method, paidAt);
        }


        public void Cancel()
        {
            if (Status == PaymentStatus.Cancelled)
                throw new PaymentAlreadyCancelledException();
            Status = PaymentStatus.Cancelled;
        }

        public static Payment Reconstitue(
            Guid id,
            Guid invoiceId,
            PaymentStatus status,
            decimal amount,
            Currency currency,
            PaymentMethod method,
            DateTime paidAt) => new(id, invoiceId, status, amount, currency, method, paidAt);
    }
}
