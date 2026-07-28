using Billing.Core.Application.Commands.MakePayment;
using Billing.Core.Domain.Enum;
using Shared.Domain.Vo;

namespace Billing.Tests.Units.Builders
{
    public class MakePaymentCommandBuilder
    {
        private Guid _invoiceId = Guid.NewGuid();
        private decimal _amount = 1000.00m;
        private Currency _currency = Currency.Xaf;
        private PaymentMethod _paymentMethod = PaymentMethod.MobileMoney;


        public MakePaymentCommandBuilder WithInvoiceId(Guid invoiceId)
        {
            _invoiceId = invoiceId;
            return this;
        }

        public MakePaymentCommandBuilder WithAmount(decimal amount)
        {
            _amount = amount;
            return this;
        }

        public MakePaymentCommandBuilder WithCurrency(Currency currency)
        {
            _currency = currency;
            return this;
        }

        public MakePaymentCommandBuilder WithPaymentMethod(PaymentMethod paymentMethod)
        {
            _paymentMethod = paymentMethod;
            return this;
        }

        public MakePaymentCommand Build() => new(_invoiceId, _amount, _currency, _paymentMethod);
    }
}
