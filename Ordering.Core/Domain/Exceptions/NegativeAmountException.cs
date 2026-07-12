using Shared.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Core.Domain.Exceptions
{
    public class NegativeAmountException() : DomainException("Le montant ne peut pas être négatif.");
    
}
