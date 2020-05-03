using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventSourcingCQRS.ReadModel.Models;
using EventSourcingCQRS.ReadModel.Persistence;
using MediatR;

namespace EventSourcingCQRS.Application.Queries
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IEnumerable<Customer>>
    {
        private readonly IReadOnlyRepository<Customer> customerRepository;

        public GetCustomersQueryHandler(IReadOnlyRepository<Customer> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> Handle(GetCustomersQuery query, CancellationToken cancellationToken)
        {
            var customers = (await customerRepository.FindAllAsync(x => true)).ToList();
            return customers;
        }
    }
}
