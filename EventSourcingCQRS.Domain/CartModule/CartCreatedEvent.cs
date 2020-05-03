using EventSourcingCQRS.Domain.Core;
using EventSourcingCQRS.Domain.CustomerModule;

namespace EventSourcingCQRS.Domain.CartModule
{
    public class CartCreatedEvent : DomainEventBase<CartId>
    {
        CartCreatedEvent()
        {
        }

        internal CartCreatedEvent(CartId aggregateId, CustomerId customerId, string cartName = null) : base(aggregateId)
        {
            CustomerId = customerId;
            CartName = cartName;
        }

        private CartCreatedEvent(CartId aggregateId, long aggregateVersion, CustomerId customerId, string cartName = null) : base(aggregateId, aggregateVersion)
        {
            CustomerId = customerId;
            CartName = cartName;
        }

        public CustomerId CustomerId { get; private set; }

        public string CartName { get; private set; }

        public override IDomainEvent<CartId> WithAggregate(CartId aggregateId, long aggregateVersion)
        {
            return new CartCreatedEvent(aggregateId, aggregateVersion, CustomerId, CartName);
        }
    }
}