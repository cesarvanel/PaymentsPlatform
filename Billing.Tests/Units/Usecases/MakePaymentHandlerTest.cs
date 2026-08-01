using Billing.Core.Application.Commands.MakePayment;
using Billing.Tests.Units.Builders;
using Billing.Tests.Units.InMemory;

namespace Billing.Tests.Units.Usecases
{
    public class MakePaymentHandlerTest
    {

        private readonly InMemoryInvoiceRepository _invoiceRepository;
        private readonly InMemoryPaymentRepository _paymentRepository;
        private readonly MakePaymentHandler _handler;

        public MakePaymentHandlerTest()
        {
            _invoiceRepository = new InMemoryInvoiceRepository();
            _paymentRepository = new InMemoryPaymentRepository();
            _handler = new MakePaymentHandler(_invoiceRepository, _paymentRepository);
        }

        [Fact]
        public async Task MakePayment_Successfully()
        {
            var invoiceId = Guid.NewGuid();
            _invoiceRepository.Initialize(invoiceId);
            var command = new MakePaymentCommandBuilder().WithInvoiceId(invoiceId).Build();
            var result = await _handler.HandleAsync(command, TestContext.Current.CancellationToken);

            Assert.True(result.IsSuccess);

        }

        [Fact]
        public async Task MakePayment_Fails_WhenInvoiceNotFound()
        {
            var command = new MakePaymentCommandBuilder().Build();
            var result = await _handler.HandleAsync(command, TestContext.Current.CancellationToken);

            Assert.False(result.IsSuccess);
        }
        
        [Fact]
        public async Task MakePayment_Fails_WhenAmountExceedsRemaining()
        {
            var invoiceId = Guid.NewGuid();
            _invoiceRepository.Initialize(invoiceId);
            var command = new MakePaymentCommandBuilder()
                .WithInvoiceId(invoiceId).WithAmount(150_000m).Build();

            var result = await _handler.HandleAsync(command, TestContext.Current.CancellationToken);
            Assert.False(result.IsSuccess);
        }


    }
}
