using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Core.Application.Commands.CreateOrder
{
    public record class CreateOrderResponse
    {
        public Guid? OrderId { get; set; } = null;
        public bool IsCreated { get; set;} = false;

    }
}
