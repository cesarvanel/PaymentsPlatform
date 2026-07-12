using Shared.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Core.Domain.Exceptions
{
    public class CurrencyMismatchException() : DomainException("La devise de la commande ne correspond pas à la devise du paiement.");

}
