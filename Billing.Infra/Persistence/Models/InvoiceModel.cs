using Billing.Core.Domain;
using Billing.Core.Domain.Enum;
using Billing.Core.Domain.Snapshot;
using Billing.Core.Domain.Vo;
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


        public static Invoice ToDomain(InvoiceModel invoiceModel)
        {
            List<InvoiceItem> invoiceItems = MapItemToDomain(invoiceModel.Items);
            List<Payment> payments = MapPaymentToDomain(invoiceModel.Payments);
            return Invoice.Reconstitue(
                invoiceModel.Id, 
                invoiceModel.OrderId, 
                invoiceItems, 
                invoiceModel.TotalAmount, 
                invoiceModel.Currency,
                payments, 
                invoiceModel.State);
        }

        public static InvoiceModel ToPersistence(InvoiceSnapshot snapshot)
        {

            return new InvoiceModel
            {
                TotalAmount = snapshot.TotalAmount,
                Currency = snapshot.Currency,
                Id = snapshot.InvoiceId,
                OrderId = snapshot.OrderId,
                Items = [.. snapshot.InvoiceItemSnapshot.Select(item =>
                                                     new InvoiceItemModel {
                                                         ProductId = item.ProductId,
                                                         Currency = item.Currency,
                                                         ProductName = item.ProductName,
                                                         ProductPrice = item.Amount,
                                                         Quantity = item.Quantity
                                                     } )],


                Payments = [.. snapshot.PaymentsSnapshot.Select(p => PaymentModel.ToPersistence(p))],
                State = snapshot.State,
            };

            
        }

        private static List<InvoiceItem> MapItemToDomain(List<InvoiceItemModel> invoiceItemModels)
        {
            return [.. invoiceItemModels.Select(item => new InvoiceItem(
                item.ProductId, item.ProductName, 
                new Quantity(item.Quantity), 
                new Money(item.ProductPrice, item.Currency)))];
        }

        private static List<Payment> MapPaymentToDomain(List<PaymentModel> paymentModels)
        {
            var query = from paymentModel in paymentModels
                        let payment = PaymentModel.ToDomain(paymentModel)
                        select payment;

            return [.. query];
        }
    }

    public class InvoiceItemModel
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }

        public Currency Currency { get; set; }

    }
}
