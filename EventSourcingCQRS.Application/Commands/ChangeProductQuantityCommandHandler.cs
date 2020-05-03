using EventSourcingCQRS.Domain.CartModule;
using EventSourcingCQRS.Domain.Persistence;
using EventSourcingCQRS.Domain.ProductModule;
using System.Threading;
using System.Threading.Tasks;
using EventSourcingCQRS.Application.Common;

namespace EventSourcingCQRS.Application.Commands
{
    public class ChangeProductQuantityCommandHandler : CommandHandler<Cart, CartId, ChangeProductQuantityCommand>
    {
        public ChangeProductQuantityCommandHandler(IRepository<Cart, CartId> cartRepository) : 
            base(cartRepository)
        {
        }

        public override CartId GetId(ChangeProductQuantityCommand request)
        {
            return new CartId(request.CartId);
        }

        public override Task<CommandResult> ChangeState(Cart aggregate, ChangeProductQuantityCommand request, CancellationToken cancellationToken)
        {
            aggregate.ChangeProductQuantity(new ProductId(request.ProductId), request.Quantity);
            return Task.FromResult(new CommandResult());
        }
    }
}
