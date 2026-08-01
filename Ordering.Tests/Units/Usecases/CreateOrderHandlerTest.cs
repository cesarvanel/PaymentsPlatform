using Ordering.Core.Application.Commands.CreateOrder;
using Ordering.Core.Domain.Events;
using Ordering.Tests.Units.Builder;
using Ordering.Tests.Units.InMemory;


namespace Ordering.Tests.Units.Usecases
{
    public class CreateOrderHandlerTest
    {

        private readonly InMemoryOrderRepository _orderRepository;
        private readonly InMemoryProductRepository _productRepository;
        private readonly CreateOrderHandler _handler;


        public CreateOrderHandlerTest()
        {
            _orderRepository = new InMemoryOrderRepository();
            _productRepository = new InMemoryProductRepository();
            _handler = new CreateOrderHandler(_orderRepository, _productRepository);
        }

        [Fact]
        public async Task CreateOrder_Fails_WhenProductNotExists()
        {
            var command = new CreateOrderCommandBuilder()
                .WithItems(Guid.NewGuid(), 2)
                .Build();

            var result = await _handler.HandleAsync(command, TestContext.Current.CancellationToken);

            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Error);
        }

        [Fact]
        public async Task CreateOrder_Successfully()
        {
            var productId = Guid.NewGuid();
            var product = new ProductBuilder().WithId(productId).Build();

            _productRepository.Initialize(product);

            var command = new CreateOrderCommandBuilder().WithItems(productId, 2).Build();

            var result = await _handler.HandleAsync(command, TestContext.Current.CancellationToken);

            var order = Assert.Single(_orderRepository.GetAll());

            Assert.Single(order.DomainEvents);
            Assert.IsType<OrderCreated>(order.DomainEvents[0]);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.True(result.Value.IsCreated);

        }
    
    }
}
