using System.Collections.Generic;
using EventSourcingCQRS.ReadModel.Models;
using MediatR;

namespace EventSourcingCQRS.Application.Queries
{
    public class GetCustomersQuery : IRequest<IEnumerable<Customer>>
    {
    }
}
