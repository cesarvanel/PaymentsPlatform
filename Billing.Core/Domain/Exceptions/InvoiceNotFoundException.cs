using Shared.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Billing.Core.Domain.Exceptions
{
    public class InvoiceNotFoundException(string? message) : DomainException(message ?? "Cette commande n'existe pas");
}
