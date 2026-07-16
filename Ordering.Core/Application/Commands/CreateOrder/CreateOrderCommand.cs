using Shared.Application.Cqrs.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Core.Application.Commands.CreateOrder
{
    public record class CreateOrderCommand(List<CreateOrderCommandSubCommand> CreateOrderCommandSubCommand): ICommand<CreateOrderResponse>;

    public record class CreateOrderCommandSubCommand(Guid ProductId, int Quantity);
}
