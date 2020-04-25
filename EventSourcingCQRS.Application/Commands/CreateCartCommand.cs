using EventSourcingCQRS.Application.Common;
using MediatR;

namespace EventSourcingCQRS.Application.Commands
{
    public class CreateCartCommand : IRequest<CommandResult>
    {
        public string CustomerId { get; private set; }

        public CreateCartCommand(string customerId)
        {
            CustomerId = customerId;
        }
    }
}
