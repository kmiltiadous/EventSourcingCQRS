using EventSourcingCQRS.Domain.CartModule;
using EventSourcingCQRS.Domain.Persistence;
using EventSourcingCQRS.Domain.ProductModule;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcingCQRS.Application.Commands
{
    public class AddProductCommandHandler : CommandHandlerBase<Cart, CartId, GetCartDetailsCommand>
    {
        public AddProductCommandHandler(IRepository<Cart, CartId> cartRepository) : 
            base(cartRepository)
        {
        }

        public override CartId GetId(GetCartDetailsCommand request)
        {
            return new CartId(request.CartId);
        }

        public override Task ChangeState(Cart aggregate, GetCartDetailsCommand request, CancellationToken cancellationToken)
        {
            var cartItem = new CartItem(new ProductId(request.ProductId), request.Quantity);
            aggregate.AddProduct(cartItem);
            return Task.CompletedTask;
        }
    }
}
