using Shared.Application.Cqrs.interfaces;
using Shared.Application.Messaging;
using Ordering.Core.Domain.Exceptions;
using Ordering.Core.Domain;
using Ordering.Core.Domain.Vo;
using Ordering.Core.Application.Ports.Out;

namespace Ordering.Core.Application.Commands.CreateOrder
{
    public sealed class CreateOrderHandler(IOrderRepository  orderRepository, IProductRepository productRepository) : ICommandHandler<CreateOrderCommand, CreateOrderResponse>
    {
        public async Task<Result<CreateOrderResponse>> HandleAsync(CreateOrderCommand command, CancellationToken ct = default)
        {
            var response = new CreateOrderResponse();

            Order order;

            try
            {
                if (command.CreateOrderCommandSubCommand.Count == 0)
                    return Result<CreateOrderResponse>.Failure("La commande doit contenir au moins un élément.");
                var orderItems = new List<OrderItem>();

                foreach (var subCommandItem in command.CreateOrderCommandSubCommand)
                {
                    var currentProduct = await productRepository.GetByIdAsync(subCommandItem.ProductId) ?? throw new ProductNotFoundException();
                    orderItems.Add(new OrderItem(currentProduct, new Quantity(subCommandItem.Quantity)));
                }

                order = Order.Create(Guid.NewGuid());

                foreach (var orderItem in orderItems)
                {
                    order.AddOrderItem(orderItem);
                }
            }
            catch (Exception ex)
                when (ex is ProductNotFoundException || ex is QuantityValueException)
            {
                return Result<CreateOrderResponse>.Failure(ex.Message);
            }


            try
            {

                order.Validate();
                await orderRepository.SaveAsync(order);
            }
            catch (InvalidOrderException ex)
            {
                return Result<CreateOrderResponse>.Failure(ex.Message);

            }

            response.IsCreated = true;
            response.OrderId = order.Id;
            return Result<CreateOrderResponse>.Success(response);

        }


    }
}
