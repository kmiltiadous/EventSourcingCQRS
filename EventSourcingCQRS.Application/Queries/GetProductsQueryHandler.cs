using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventSourcingCQRS.ReadModel.Models;
using EventSourcingCQRS.ReadModel.Persistence;
using MediatR;

namespace EventSourcingCQRS.Application.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IReadOnlyRepository<Product> productRepository;

        public GetProductsQueryHandler(IReadOnlyRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = (await productRepository.FindAllAsync(x => true)).ToList();
            return products;
        }
    }
}
