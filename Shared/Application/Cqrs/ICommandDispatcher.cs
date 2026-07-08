using Shared.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Application.Cqrs
{
    public interface ICommandDispatcher
    {
        Task<Result<TResponse>> ExecuteAsync<TResponse>(ICommand<TResponse> command, CancellationToken ct = default);
    }
}
