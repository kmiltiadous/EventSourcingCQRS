using System;
using EventSourcingCQRS.Domain.Core;
using EventSourcingCQRS.Domain.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using EventSourcingCQRS.Application.Common;

namespace EventSourcingCQRS.Application.Commands
{
    public abstract class
        CommandHandler<TAggregate, TAggregateId, TCommandRequest> :
            CommandHandlerBase<TAggregate, TAggregateId, TCommandRequest, CommandResult>,
            IRequestHandler<TCommandRequest, CommandResult>
        where TAggregate : AggregateBase<TAggregateId>
        where TAggregateId : IAggregateId
        where TCommandRequest : IRequest<CommandResult>
    {

        protected CommandHandler(
            IRepository<TAggregate, TAggregateId> aggregateRepository) : base(aggregateRepository)
        {}

    }

    public abstract class GenericResponseCommandHandler<TAggregate, TAggregateId, TCommandRequest, TCommandResult> : 
        CommandHandlerBase<TAggregate, TAggregateId, TCommandRequest, TCommandResult>,
        IRequestHandler<TCommandRequest, TCommandResult>
        where TAggregate : AggregateBase<TAggregateId>
        where TAggregateId : IAggregateId
        where TCommandResult : CommandResult<TAggregateId>
        where TCommandRequest : IRequest<TCommandResult>
    {

        protected GenericResponseCommandHandler(
            IRepository<TAggregate, TAggregateId> aggregateRepository) :base(aggregateRepository)
        {}
    }

    public abstract class CommandHandlerBase<TAggregate, TAggregateId, TCommandRequest, TResult> 
        where TAggregate : AggregateBase<TAggregateId> 
        where TResult : CommandResult
    
    {
        private readonly IRepository<TAggregate, TAggregateId> aggregateRepository;
    
        protected CommandHandlerBase(
            IRepository<TAggregate, TAggregateId> aggregateRepository)
        {
            this.aggregateRepository = aggregateRepository;
        }
    
        public virtual async Task<TAggregate> GetOrCreate(TCommandRequest request)
        {
            var id = GetId(request);
            var aggregate = await aggregateRepository.GetByIdAsync(id);
            return aggregate;
        }
        public abstract TAggregateId GetId(TCommandRequest request);
    
        public abstract Task<TResult> ChangeState(TAggregate aggregate, TCommandRequest request,
            CancellationToken cancellationToken);
        public virtual async Task<TResult> Handle(TCommandRequest request, CancellationToken cancellationToken)
        {
            var aggregate = await GetOrCreate(request);
            try
            {
                var result = await ChangeState(aggregate, request, cancellationToken);
                await SaveState(aggregate);
    
                return result;
            }
            catch (BusinessRuleException<TAggregate> e)
            {
                if (!string.IsNullOrEmpty(e.Message))
                {
                    return (TResult)Activator.CreateInstance(typeof(TResult), args: new BrokenRule(e.Message));
                }
    
                return (TResult)Activator.CreateInstance(typeof(TResult), args: new BrokenRule("Business rule exception"));
            }
        }
    
        private async Task SaveState(TAggregate aggregate)
        {
            await aggregateRepository.SaveAsync(aggregate);
        }
    }
}
