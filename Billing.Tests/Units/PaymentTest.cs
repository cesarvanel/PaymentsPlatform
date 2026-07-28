

using Billing.Core.Application.Commands.MakePayment;
using Billing.Tests.Units.Builders;
using Billing.Tests.Units.InMemory;

namespace Billing.Tests.Units
{
    public class PaymentTest
    {

        [Fact]
        public async Task Handle_MakePayment_Successfully()
        {
            var invoiceId = Guid.NewGuid();
            var invoiceRepository = new InMemoryInvoiceRepository();
            invoiceRepository.Initialize(invoiceId);
            var paymentRepository = new InMemoryPaymentRepository();
                
            var handler = new MakePaymentHandler(invoiceRepository, paymentRepository);
            var command = new MakePaymentCommandBuilder().WithInvoiceId(invoiceId).Build();
            var result = await handler.HandleAsync(command, TestContext.Current.CancellationToken);

            Assert.True(result.IsSuccess);

        }

        [Fact]
        public async Task Handle_MakePayment_WhenInvoiceNotFound()
        {
            var invoiceRepository = new InMemoryInvoiceRepository();
            var paymentRepository = new InMemoryPaymentRepository();

            var handler = new MakePaymentHandler(invoiceRepository, paymentRepository);
            var command = new MakePaymentCommandBuilder().Build();
            var result = await handler.HandleAsync(command, TestContext.Current.CancellationToken);

            Assert.False(result.IsSuccess);
        }
    }
}
