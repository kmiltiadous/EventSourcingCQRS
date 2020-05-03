using System.Threading;
using System.Threading.Tasks;
using EventSourcingCQRS.ReadModel.Models;
using EventSourcingCQRS.ReadModel.Persistence;
using MediatR;

namespace EventSourcingCQRS.Application.Queries
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, Cart>
    {
        private readonly IReadOnlyRepository<Cart> cartRepository;

        public GetCartQueryHandler(IReadOnlyRepository<Cart> cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public async Task<Cart> Handle(GetCartQuery query, CancellationToken cancellationToken)
        {
            var cart = await cartRepository.GetByIdAsync(query.Id);
            return cart;
        }
    }
}
