using EventSourcingCQRS.Domain.CartModule;
using EventSourcingCQRS.Domain.Persistence;
using EventSourcingCQRS.Domain.ProductModule;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcingCQRS.Application.Commands
{
    public class ChangeProductQuantityCommandHandler : CommandHandlerBase<Cart, CartId, ChangeProductQuantityCommand>
    {
        public ChangeProductQuantityCommandHandler(IRepository<Cart, CartId> cartRepository) : 
            base(cartRepository)
        {
        }

        public override CartId GetId(ChangeProductQuantityCommand request)
        {
            return new CartId(request.CartId);
        }

        public override Task ChangeState(Cart aggregate, ChangeProductQuantityCommand request, CancellationToken cancellationToken)
        {
            aggregate.ChangeProductQuantity(new ProductId(request.ProductId), request.Quantity);
            return Task.CompletedTask;
        }
    }
}
