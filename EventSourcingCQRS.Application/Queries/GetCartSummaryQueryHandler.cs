using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventSourcingCQRS.ReadModel.Models;
using EventSourcingCQRS.ReadModel.Persistence;
using MediatR;

namespace EventSourcingCQRS.Application.Queries
{
    public class GetCartSummaryQueryHandler : IRequestHandler<GetCartSummaryQuery, CartSummary>
    {
        private readonly IReadOnlyRepository<Cart> cartRepository;
        private readonly IReadOnlyRepository<Customer> customerRepository;

        public GetCartSummaryQueryHandler(IReadOnlyRepository<Cart> cartRepository,
            IReadOnlyRepository<Customer> customerRepository,
            IReadOnlyRepository<Product> productRepository)
        {
            this.cartRepository = cartRepository;
            this.customerRepository = customerRepository;
        }

        public async Task<CartSummary> Handle(GetCartSummaryQuery query, CancellationToken cancellationToken)
        {
            var carts = (await cartRepository.FindAllAsync(x => true)).ToList();
            var customers = (await customerRepository.FindAllAsync(x => true)).ToList();

            var details = new CartSummary
            {
                Carts = carts,
                Customers = customers
            };

            return details;
        }
    }
}
