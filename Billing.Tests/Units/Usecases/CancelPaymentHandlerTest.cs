using Billing.Core.Application.Commands.CancelPayment;
using Billing.Core.Domain;
using Billing.Core.Domain.Enum;
using Billing.Tests.Units.Builders;
using Billing.Tests.Units.InMemory;
using Shared.Domain.Vo;

namespace Billing.Tests.Units.Usecases
{
    public class CancelPaymentHandlerTest
    {
        private readonly InMemoryInvoiceRepository _invoiceRepository;
        private readonly CancelPaymentHandler _handler;


        public CancelPaymentHandlerTest()
        {
            _invoiceRepository = new InMemoryInvoiceRepository();
            _handler = new CancelPaymentHandler(_invoiceRepository);
        }


        [Fact]
        public async Task CancelPaymentSuccessfully()
        {
            var paymentOneId = Guid.NewGuid();
            var paymentTwoId = Guid.NewGuid();
            var command = new CancelPaymentCommandBuilder().WithPaymentId(paymentOneId).Build();

            var payments = new List<Payment>
            {
                Payment.Create(paymentOneId, command.InvoiceId, 15000.00m, Currency.Xaf, PaymentMethod.Cash, DateTime.Now),
                Payment.Create(paymentTwoId, command.InvoiceId, 20000.00m, Currency.Xaf, PaymentMethod.BankTransfer, DateTime.Now),
            };
          
            _invoiceRepository.SeedWithPayment(command.InvoiceId, payments);
            var result = await _handler.HandleAsync(command, TestContext.Current.CancellationToken);

            var invoice = Assert.Single(_invoiceRepository.GetAll());



            Assert.True(result.IsSuccess, result.Error);

            Assert.Equal(InvoiceState.PartiallyPaid, invoice.State);

            Assert.Equal(PaymentStatus.Cancelled, invoice.GetAllPayments[0].Status);

            Assert.Equal(20_000.00m, invoice.PaidAmount.Value);

            Assert.Equal(15_000.00m, invoice.RemainingAmount.Value);



        }
    }
}
