using Shared.Domain.Exceptions;


namespace Ordering.Core.Domain.Exceptions
{
    public class ProductNotFoundException() : DomainException("Ce produit n'existe pas");
  
}
