using EventSourcingCQRS.ReadModel.Models;
using MediatR;

namespace EventSourcingCQRS.Application.Queries
{
    public class GetCartDetailsQuery : IRequest<CartDetails>
    {
        public string CartId { get; private set; }

        public GetCartDetailsQuery(string cartId)
        {
            CartId = cartId;
        }
    }
}
