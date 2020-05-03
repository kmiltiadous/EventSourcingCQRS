using EventSourcingCQRS.Application.Common;
using EventSourcingCQRS.Domain.CartModule;
using MediatR;

namespace EventSourcingCQRS.Application.Commands
{
    public class CreateCartCommand : IRequest<CommandResult<CartId>>
    {
        public string CustomerId { get; private set; }
        public string CartName { get; private set; }

        public CreateCartCommand(string customerId, string cartName = null)
        {
            CustomerId = customerId;
            CartName = cartName;
        }
    }
}
