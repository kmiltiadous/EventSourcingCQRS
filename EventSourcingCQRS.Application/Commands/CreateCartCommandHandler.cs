using EventSourcingCQRS.Domain.CartModule;
using EventSourcingCQRS.Domain.CustomerModule;
using EventSourcingCQRS.Domain.Persistence;
using System.Threading;
using System.Threading.Tasks;
using EventSourcingCQRS.Application.Common;

namespace EventSourcingCQRS.Application.Commands
{
    public class CreateCartCommandHandler : GenericResponseCommandHandler<Cart, CartId, CreateCartCommand, CommandResult<CartId>>
    {
        public CreateCartCommandHandler(IRepository<Cart, CartId> cartRepository) : 
            base(cartRepository)
        {
        }

        public override CartId GetId(CreateCartCommand request)
        {
            return CartId.NewCartId();
        }

        public override Task<Cart> GetOrCreate(CreateCartCommand request)
        {
            return Task.FromResult(new Cart(GetId(request), new CustomerId(request.CustomerId), request.CartName));
        }

        public override Task<CommandResult<CartId>> ChangeState(Cart aggregate, CreateCartCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new CommandResult<CartId>(aggregate.Id));
        }
    }
}
