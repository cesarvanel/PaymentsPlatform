using Billing.Core.Domain.Enum;
using Billing.Core.Domain.Exceptions;
using Billing.Core.Domain.Vo;
using Shared.Domain.Exceptions;
using Shared.Domain.Vo;
namespace Billing.Core.Domain
{
    public class Invoice
    {
        private readonly List<Payment> _payments = [];

        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public Money TotalAmount { get; }
        public InvoiceState State { get; private set; }
        public IReadOnlyList<InvoiceItem> InvoiceItems { get; } = [];

        private Invoice(
            Guid id,
            Guid orderId,
            List<InvoiceItem> invoicesItems,
            decimal totalAmount,
            Currency currency,
            List<Payment>? payments)
        {
            Id = id;
            OrderId = orderId;
            InvoiceItems = invoicesItems;
            TotalAmount = new Money(totalAmount, currency);
            _payments = payments ?? [];
            State = InvoiceState.Issued;
        }


        public static Invoice Create(
            Guid id,
            Guid orderId,
            List<InvoiceItem> invoicesItems,
            decimal totalAmount,
            Currency currency)
        {
            return new Invoice(id, orderId, invoicesItems, totalAmount, currency, null);
        }

        public Money PaidAmount => _payments
                                   .Where(p => p.Status == PaymentStatus.Active)
                                   .Aggregate(new Money(0, TotalAmount.Currency), (total, p) => total.Add(p.Amount));
        public Money RemainingAmount => TotalAmount.Subtract(PaidAmount);


        public void AddPayment(Money paidAmount, PaymentMethod method)
        {
            EnsureCanReceivePayment();
            EnsureSameCurrency(paidAmount);
            if (paidAmount.Amount > RemainingAmount.Amount) throw new PaymentExceedsRemainingException();

            var payment = Payment.Create(Guid.NewGuid(), Id, paidAmount.Amount, paidAmount.Currency, method, DateTime.UtcNow);

            _payments.Add(payment);

            State = RecalculateState();
        }


        public void CancelPayment(Guid paymentId)
        {
            EnsureCanCancelPayment();
            var payment = _payments.Find(p => p.Id == paymentId) ?? throw new PaymentNotFoundException(paymentId);
            payment.Cancel();
            State = RecalculateState();


        }

        private InvoiceState RecalculateState()
        {
            if (PaidAmount.Amount == 0) return InvoiceState.Issued; 
            if (RemainingAmount.Amount > 0) return InvoiceState.PartiallyPaid; 
            return InvoiceState.Paid;            
        }
        private void EnsureCanReceivePayment()
        {
            if (State == InvoiceState.Cancelled || State == InvoiceState.Paid)
            {
                throw new PaymentNotAllowedException(null);
            }
        }

        private void EnsureCanCancelPayment()
        {
            if (State == InvoiceState.Cancelled)
                throw new PaymentNotAllowedException("Facture annulée, aucune opération possible.");
        }
        private void EnsureSameCurrency(Money other)
        {
            if (TotalAmount.Currency != other.Currency)
            {
                throw new CurrencyMismatchException();
            }
        }

     
    }
}
