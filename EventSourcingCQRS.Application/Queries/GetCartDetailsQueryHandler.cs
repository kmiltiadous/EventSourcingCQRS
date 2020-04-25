using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventSourcingCQRS.ReadModel.Models;
using EventSourcingCQRS.ReadModel.Persistence;
using MediatR;

namespace EventSourcingCQRS.Application.Queries
{
    public class GetCartDetailsQueryHandler : IRequestHandler<GetCartDetailsQuery, CartDetails>
    {
        private readonly IReadOnlyRepository<Cart> cartRepository;
        private readonly IReadOnlyRepository<CartItem> cartItemRepository;
        private readonly IReadOnlyRepository<Product> productRepository;

        public GetCartDetailsQueryHandler(IReadOnlyRepository<Cart> cartRepository,
            IReadOnlyRepository<CartItem> cartItemRepository,
            IReadOnlyRepository<Product> productRepository)
        {
            this.cartRepository = cartRepository;
            this.cartItemRepository = cartItemRepository;
            this.productRepository = productRepository;
        }

        public async Task<CartDetails> Handle(GetCartDetailsQuery query, CancellationToken cancellationToken)
        {
            var cart = await cartRepository.GetByIdAsync(query.CartId);
            var cartItems = (await cartItemRepository.FindAllAsync(x => x.CartId == query.CartId)).ToList();
            var products = (await productRepository.FindAllAsync(x => true)).ToList();
            var details = new CartDetails
            {
                Cart = cart,
                CartItems = cartItems,
                Products = products
            };

            return details;
        }
    }
}
