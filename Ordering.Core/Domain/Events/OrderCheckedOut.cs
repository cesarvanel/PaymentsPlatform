using Shared.Domain.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Core.Domain.Events
{
    public record OrderCheckedOut(Guid OrderId, decimal Total) : IAuditableEvent;

}
