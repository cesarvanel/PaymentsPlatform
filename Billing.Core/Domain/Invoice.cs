using Billing.Core.Domain.Enum;
using Billing.Core.Domain.Exceptions;
using Billing.Core.Domain.Snapshot;
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
            List<Payment> payments,
            InvoiceState state)
        {
            Id = id;
            OrderId = orderId;
            InvoiceItems = invoicesItems;
            TotalAmount = new Money(totalAmount, currency);
            _payments = payments;
            State = state;
        }


        public static Invoice Create(
            Guid id,
            Guid orderId,
            List<InvoiceItem> invoicesItems,
            decimal totalAmount,
            Currency currency)
        {
            return new Invoice(id, orderId, invoicesItems, totalAmount, currency, [], InvoiceState.Issued);
        }

        public Money PaidAmount => _payments
                                   .Where(p => p.Status == PaymentStatus.Active)
                                   .Aggregate(new Money(0, TotalAmount.Currency), (total, p) => total.Add(p.Amount));
        public Money RemainingAmount => TotalAmount.Subtract(PaidAmount);

        public IReadOnlyList<Payment> Payments => _payments;

     

        public Payment AddPayment(Money paidAmount, PaymentMethod method)
        {
            EnsureCanReceivePayment();
            EnsureSameCurrency(paidAmount);
            if (paidAmount.Value > RemainingAmount.Value) throw new PaymentExceedsRemainingException();

            var payment = Payment.Create(Guid.NewGuid(), Id, paidAmount.Value, paidAmount.Currency, method, DateTime.UtcNow);

            _payments.Add(payment);
            State = RecalculateState();

            return payment;

        }


        public InvoiceSnapshot ToSnapshot()
        {
            var invoiceItemsSnapshot = InvoiceItems.Select(item =>
            new InvoiceItemSnapshot(
                                    Amount: item.Price.Value,
                                    Currency: item.Price.Currency,
                                    ProductId: item.ProductId,
                                    ProductName: item.Name,
                                    item.Quantity.Value)).ToList();

            var paymentsSnapshot = _payments.Select(p => p.ToSnapShot()).ToList();

            return new InvoiceSnapshot(
                 Id,
                 OrderId,
                 TotalAmount.Value,
                 TotalAmount.Currency,
                 State,
                 invoiceItemsSnapshot,
                 paymentsSnapshot);
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
            if (PaidAmount.Value == 0.0m) return InvoiceState.Issued;
            if (RemainingAmount.Value > 0.0m) return InvoiceState.PartiallyPaid;
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

        public static Invoice Reconstitue(
            Guid id,
            Guid orderId,
            List<InvoiceItem> invoicesItems,
            decimal totalAmount,
            Currency currency,
            List<Payment> payments,
            InvoiceState state
            )

        {
            return new(id, orderId, invoicesItems, totalAmount, currency, payments, state);
        }


    }
}




