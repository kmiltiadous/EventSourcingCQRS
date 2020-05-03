using System.Collections.Generic;
using EventSourcingCQRS.ReadModel.Models;
using MediatR;

namespace EventSourcingCQRS.Application.Queries
{
    public class GetCartQuery : IRequest<Cart>
    {
        public string Id { get; }
        public GetCartQuery(string id)
        {
            Id = id;
        }
    }
}
