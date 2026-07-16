

using Shared.Application.Cqrs.implemetations;
using Shared.Application.Cqrs.interfaces;
using Shared.Application.Messaging;
using Shared.Application.providers;
using Shared.Test.InMemory;

namespace Shared.Test
{
    public class CommandDispatcherTests
    {
        private record PingCommand(string Message) : ICommand<string>;

        private sealed class PingHandler : ICommandHandler<PingCommand, string>
        {
            public Task<Result<string>> HandleAsync(PingCommand command, CancellationToken ct = default)
            {
                return Task.FromResult(Result<string>.Success($"ping : {command.Message}"));
            }

        }


        private sealed class TestResolver : IServiceResolver
        {
            public T Resolve<T>() where T : notnull
            {
                if (typeof(T) == typeof(ICommandHandler<PingCommand, string>))
                {
                    return (T)(object)new PingHandler();
                }

                throw new InvalidOperationException($"Pas de service pour {typeof(T)}");

            }
        }

        private sealed class NullLogger<T> : IAppLogger<T>
        {
            public void Info(string m, params object[] a) { }
            public void Warn(string m, params object[] a) { }
            public void Error(Exception ex, string m, params object[] a) { }
        }

        [Fact]
        public async Task ExectueAsync_RunsHandler_AndReturnnResukt()
        {
            var dispatcher = new CommandDispatcher(
                new TestResolver(),
                new InMemoryUnitOfWork(),
                new DomainEventDispatcher([]),
                new InMemoryDomainEventStore(),
                new InMemoryOutbox(),
                new NullLogger<CommandDispatcher>());


            var result = await dispatcher.ExecuteAsync(new PingCommand("Connection"), TestContext.Current.CancellationToken);

            Console.WriteLine(result);

            Assert.True(result.IsSuccess);

        }


    }
}
