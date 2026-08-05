using Billing.Core.Domain.Enum;
using Shared.Domain;
using Shared.Domain.Vo;

namespace Billing.Infra.Persistence.Models
{
    public class InvoiceModel : AuditableModel
    {
        public const string TableName = "invoices";
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public Currency Currency { get; set; }

        public List<InvoiceItemModel> Items { get; set; } = [];

        public InvoiceState State { get; set; }

        public List<PaymentModel> Payments { get; set; } = [];
    }

    public class InvoiceItemModel
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }

    }
}
