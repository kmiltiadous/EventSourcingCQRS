using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventSourcingCQRS.ReadModel.Models;
using EventSourcingCQRS.ReadModel.Persistence;
using MediatR;

namespace EventSourcingCQRS.Application.Queries
{
    public class GetCartItemsQueryHandler : IRequestHandler<GetCartItemsQuery, IEnumerable<CartItem>>
    {
        private readonly IReadOnlyRepository<CartItem> cartItemRepository;

        public GetCartItemsQueryHandler(IReadOnlyRepository<CartItem> cartItemRepository)
        {
            this.cartItemRepository = cartItemRepository;
        }

        public async Task<IEnumerable<CartItem>> Handle(GetCartItemsQuery query, CancellationToken cancellationToken)
        {
            var cartItems = (await cartItemRepository.FindAllAsync(x => x.CartId == query.CartId)).ToList();

            return cartItems;
        }
    }
}
