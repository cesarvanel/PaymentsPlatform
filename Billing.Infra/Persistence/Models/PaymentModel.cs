using Billing.Core.Domain;
using Billing.Core.Domain.Enum;
using Billing.Core.Domain.Snapshot;
using Shared.Domain;
using Shared.Domain.Vo;

namespace Billing.Infra.Persistence.Models
{
    public class PaymentModel : AuditableModel
    {

        public const string TableName = "payments";
        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }

        public DateTime PaidAt { get; set; }

        public static Payment ToDomain(PaymentModel paymentModel)
        {

            return Payment.Reconstitue(
                paymentModel.Id,
                paymentModel.InvoiceId,
                paymentModel.Status,
                paymentModel.Amount,
                paymentModel.Currency,
                paymentModel.Method, 
                paymentModel.PaidAt);
        }


        public static PaymentModel ToPersistence(PaymentSnapshot snapshot)
        {

            return new PaymentModel
            {
                Id = snapshot.PaymentId,
                InvoiceId = snapshot.InvoiceId,
                Amount = snapshot.Amount,
                Currency = snapshot.Currency,
                Method = snapshot.Method,
                PaidAt = snapshot.PaidAt,
                Status = snapshot.Status,
                
            };
        }

    }


}
