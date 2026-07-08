using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Application.Messaging
{
    public interface IAppLogger<T>
    {
        void Info(string message, params object[] args);
        void Error(Exception ex, string message, params object[] args);
        void Warn(string message, params object[] args);
    }
}
