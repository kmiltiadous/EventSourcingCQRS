using System.Collections.Generic;
using EventSourcingCQRS.ReadModel.Models;
using MediatR;

namespace EventSourcingCQRS.Application.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
