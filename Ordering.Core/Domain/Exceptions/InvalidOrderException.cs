using Shared.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Core.Domain.Exceptions
{
    public class InvalidOrderException(string message) : DomainException(message);
}
