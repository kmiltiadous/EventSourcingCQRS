using EventSourcingCQRS.Domain.CartModule;
using EventSourcingCQRS.Domain.Persistence;
using EventSourcingCQRS.Domain.ProductModule;
using System.Threading;
using System.Threading.Tasks;
using EventSourcingCQRS.Application.Common;

namespace EventSourcingCQRS.Application.Commands
{
    public class AddProductCommandHandler : CommandHandler<Cart, CartId, AddProductCommand>
    {
        public AddProductCommandHandler(IRepository<Cart, CartId> cartRepository) : 
            base(cartRepository)
        {
        }

        public override CartId GetId(AddProductCommand request)
        {
            return new CartId(request.CartId);
        }

        public override Task<CommandResult> ChangeState(Cart aggregate, AddProductCommand request, CancellationToken cancellationToken)
        {
            var cartItem = new CartItem(new ProductId(request.ProductId), request.Quantity);
            aggregate.AddProduct(cartItem);
            return Task.FromResult(new CommandResult());
        }
    }
}
