using Shared.Domain.Exceptions;

namespace Ordering.Core.Domain.Exceptions
{
    public class QuantityValueException() : DomainException("La quantité doit être supérieure ou égale à 1.");
  
}
