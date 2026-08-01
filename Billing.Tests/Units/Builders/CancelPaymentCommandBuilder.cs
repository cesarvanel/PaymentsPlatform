using Billing.Core.Application.Commands.CancelPayment;

namespace Billing.Tests.Units.Builders
{
    public class CancelPaymentCommandBuilder
    {
        private Guid _invoiceId = Guid.NewGuid();
        private Guid _paymentId = Guid.NewGuid();


        public CancelPaymentCommandBuilder WithInvoiceId(Guid invoiceId)
        {
            _invoiceId = invoiceId;
            return this;
        }

        public CancelPaymentCommandBuilder WithPaymentId(Guid paymentId)
        {
            _paymentId = paymentId;
            return this;
        }

        public CancelPaymentCommand Build() => new(_invoiceId, _paymentId);



    }
}
