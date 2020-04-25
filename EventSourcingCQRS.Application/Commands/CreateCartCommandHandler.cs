using EventSourcingCQRS.Domain.CartModule;
using EventSourcingCQRS.Domain.CustomerModule;
using EventSourcingCQRS.Domain.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcingCQRS.Application.Commands
{
    public class CreateCartCommandHandler : CommandHandlerBase<Cart, CartId, CreateCartCommand>
    {
        public CreateCartCommandHandler(IRepository<Cart, CartId> cartRepository) : 
            base(cartRepository)
        {
        }

        public override CartId GetId(CreateCartCommand request)
        {
            return CartId.NewCartId();
        }

        public override Task<Cart> Get(CreateCartCommand request)
        {
            return Task.FromResult(new Cart(GetId(request), new CustomerId(request.CustomerId)));
        }

        public override Task ChangeState(Cart aggregate, CreateCartCommand request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
