using EventSourcingCQRS.Application.Common;
using MediatR;

namespace EventSourcingCQRS.Application.Commands
{
    public class AddProductCommand : IRequest<CommandResult>
    {
        public string CartId { get; private set; }
        public string ProductId { get; private set; }
        public int Quantity { get; private set; }

        public AddProductCommand(string cartId, string productId, int quantity)
        {
            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
