using Shared.Application.Messaging;
using Shared.Domain.Event;
using System.Collections.Concurrent;


namespace Shared.Application.Cqrs
{
    public sealed class CommandDispatcher(
        IServiceResolver resolver,
        IUnitOfWork unitOfWork,
        IDomainEventDispatcher domainEvents,
        IDomainEventStore auditStore,
        IOutbox outbox,
        IAppLogger<CommandDispatcher> logger) : ICommandDispatcher
    {
        private const int MaxEventDrainLoops = 100; 
        private static readonly ConcurrentDictionary<Type , object> _wrappers = new ();

        public async Task<Result<TResponse>> ExecuteAsync<TResponse>(ICommand<TResponse> command, CancellationToken ct = default)
        {

            var commandType = command.GetType();

            logger.Info("==> Début : {0}", commandType.Name);

            var wrapper = (CommandHandlerWrapper<TResponse>)_wrappers.GetOrAdd(commandType, static (type, resp) =>
            {
                var wrapperType = typeof(CommandHandlerWrapper<,>).MakeGenericType(type, resp);
                return Activator.CreateInstance(wrapperType)!;

            }, typeof(TResponse));


            await unitOfWork.BeginTransactionAsync(ct);

            try {
                var result = await wrapper.HandleAsync(command, resolver, ct); 


                await unitOfWork.SaveChangesAsync(ct);

                var allProcessed = await DrainDomainEventsAsync(ct);

                await auditStore.AppendAsync(allProcessed.OfType<IAuditableEvent>(), ct);
                await outbox.AddAsync(allProcessed.OfType<IIntegrationEvent>(), ct);
                await unitOfWork.SaveChangesAsync(ct);
                await unitOfWork.CommitAsync(ct);

                logger.Info("==> Fin : {0}", commandType.Name);

                return result;

            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync(ct);
                logger.Error(ex, "Rollback : {0}", commandType.Name);

                throw;
            }
        }



        private async Task<List<IDomainEvent>> DrainDomainEventsAsync(CancellationToken ct)
        {
            var allProcessed = new List<IDomainEvent>();
            var newEvents = unitOfWork.CollectDomainEvents(); 

            int loops = MaxEventDrainLoops;
            while (newEvents.Count > 0)
            {
                if (--loops <= 0)
                    throw new InvalidOperationException(
                        "Trop d'itérations de publication d'events — cascade infinie probable.");

                await domainEvents.PublishAsync(newEvents, ct); 
                allProcessed.AddRange(newEvents);

                newEvents = unitOfWork.CollectDomainEvents();   
            }

            return allProcessed;
        }
    }
}


