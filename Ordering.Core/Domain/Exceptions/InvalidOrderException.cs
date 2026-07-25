using Shared.Domain.Exceptions;

namespace Ordering.Core.Domain.Exceptions
{
    public class InvalidOrderException(string message) : DomainException(message);
}
