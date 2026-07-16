using Shared.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Core.Domain.Exceptions
{
    public class ProductNotFoundException() : DomainException("Ce produit n'existe pas");
  
}
