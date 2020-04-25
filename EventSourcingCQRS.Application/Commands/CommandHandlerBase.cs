using EventSourcingCQRS.Domain.Core;
using EventSourcingCQRS.Domain.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using EventSourcingCQRS.Application.Common;

namespace EventSourcingCQRS.Application.Commands
{
    public abstract class
        CommandHandlerBase<TAggregate, TAggregateId, TCommandRequest> : IRequestHandler<TCommandRequest, CommandResult>
        where TAggregate : AggregateBase<TAggregateId>
        where TAggregateId : IAggregateId
        where TCommandRequest : IRequest<CommandResult>
    {
        private readonly IRepository<TAggregate, TAggregateId> _aggregateRepository;

        protected CommandHandlerBase(
            IRepository<TAggregate, TAggregateId> aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public abstract TAggregateId GetId(TCommandRequest request);

        public abstract Task ChangeState(TAggregate aggregate, TCommandRequest request,
            CancellationToken cancellationToken);

        public virtual async Task<TAggregate> Get(TCommandRequest request)
        {
            var id = GetId(request);
            var aggregate = await _aggregateRepository.GetByIdAsync(id);
            return aggregate;
        }

        public async Task<CommandResult> Handle(TCommandRequest request, CancellationToken cancellationToken)
        {
            var aggregate = await Get(request);
            try
            {
                await ChangeState(aggregate, request, cancellationToken);
                await SaveState(aggregate);

                return new CommandResult();
            }
            catch (BusinessRuleException<TAggregate> e)
            {
                if (!string.IsNullOrEmpty(e.Message))
                {
                    return new CommandResult(new BrokenRule(e.Message));
                }

                return new CommandResult(new BrokenRule("Business rule exception"));
            }
        }

        private async Task SaveState(TAggregate aggregate)
        {
            await _aggregateRepository.SaveAsync(aggregate);
        }
    }
}
