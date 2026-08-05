using Billing.Core.Domain.Enum;
using Shared.Domain;
using Shared.Domain.Vo;

namespace Billing.Infra.Persistence.Models
{
    public class PaymentModel: AuditableModel
    {

        public const string TableName = "payments";
        public Guid Id { get; set; }
        public Guid InvoiceId {  get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }

        public DateTime PaidAt { get; set; }

    }
}
