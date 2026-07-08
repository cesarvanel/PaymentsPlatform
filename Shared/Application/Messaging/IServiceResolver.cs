using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Application.Messaging
{
    public interface IServiceResolver
    {
        T Resolve<T>() where T : notnull;

    }
}
