using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventSourcingCQRS.ReadModel.Models;
using EventSourcingCQRS.ReadModel.Persistence;
using MediatR;

namespace EventSourcingCQRS.Application.Queries
{
    public class GetCartsQueryHandler : IRequestHandler<GetCartsQuery, IEnumerable<Cart>>
    {
        private readonly IReadOnlyRepository<Cart> cartRepository;

        public GetCartsQueryHandler(IReadOnlyRepository<Cart> cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        public async Task<IEnumerable<Cart>> Handle(GetCartsQuery query, CancellationToken cancellationToken)
        {
            var carts = (await cartRepository.FindAllAsync(x => true)).ToList();
            return carts;
        }
    }
}
