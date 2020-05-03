using System.Collections.Generic;
using EventSourcingCQRS.ReadModel.Models;
using MediatR;

namespace EventSourcingCQRS.Application.Queries
{
    public class GetCartItemsQuery : IRequest<IEnumerable<CartItem>>
    {
        public string CartId { get; private set; }

        public GetCartItemsQuery(string cartId)
        {
            CartId = cartId;
        }
    }
}
