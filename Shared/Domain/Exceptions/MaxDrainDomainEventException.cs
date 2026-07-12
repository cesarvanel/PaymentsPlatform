using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Domain.Exceptions
{
    public class MaxDrainDomainEventException(): DomainException("Trop d'itérations de publication d'events — cascade infinie probable.")
    {
    }
}
