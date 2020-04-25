using EventSourcingCQRS.Domain.Core;
using System;
using EventSourcingCQRS.Domain.CustomerModule;
using EventSourcingCQRS.Domain.ProductModule;
using System.Collections.Generic;
using System.Linq;
using EventSourcingCQRS.Domain.Validation;

namespace EventSourcingCQRS.Domain.CartModule
{
    public class Cart : AggregateBase<CartId>
    {
        public const int ProductQuantityThreshold = 50;

        //Needed for persistence purposes
        private Cart()
        {
            Items = new List<CartItem>();
        }

        private CustomerId CustomerId { get; set; }

        private List<CartItem> Items { get; set; }

        public Cart(CartId cartId, CustomerId customerId) : this()
        {
            if (cartId == null)
            {
                throw new ArgumentNullException(nameof(cartId));
            }
            if (customerId == null)
            {
                throw new ArgumentNullException(nameof(customerId));
            }
            RaiseEvent(new CartCreatedEvent(cartId, customerId));
        }

        public void AddProduct(CartItem cartItem)
        {
            var validator = new ProductInInventoryValidator().And(new CartItemNotInCartValidator(Items));

            var result = validator.Validate(cartItem);
            if (!result.IsValid)
            {
                throw new CartException( result.Errors.First().ErrorMessage);
            }

            RaiseEvent(new ProductAddedEvent(cartItem.ProductId, cartItem.Quantity));
        }

        public void ChangeProductQuantity(ProductId productId, int quantity)
        {
            var validator = new ProductInInventoryValidator().And(new CartItemInCartValidator(Items));

            var result = validator.Validate(new CartItem(productId, quantity));
            if (!result.IsValid)
            {
                throw new CartException(result.Errors.First().ErrorMessage);
            }

            RaiseEvent(new ProductQuantityChangedEvent(productId, GetCartItemByProduct(productId).Quantity, quantity));
        }

        public override string ToString()
        {
            return $"{{ Id: \"{Id}\", CustomerId: \"{CustomerId.IdAsString()}\", Items: [{string.Join(", ", Items.Select(x => x.ToString()))}] }}";
        }

        internal void Apply(CartCreatedEvent ev)
        {
            Id = ev.AggregateId;
            CustomerId = ev.CustomerId;
        }

        internal void Apply(ProductAddedEvent ev)
        {
            Items.Add(new CartItem(ev.ProductId, ev.Quantity));
        }

        internal void Apply(ProductQuantityChangedEvent ev)
        {
            var existingItem = Items.Single(x => x.ProductId == ev.ProductId);

            Items.Remove(existingItem);
            Items.Add(existingItem.WithQuantity(ev.NewQuantity));
        }

        private CartItem GetCartItemByProduct(ProductId productId)
        {
            return Items.Single(x => x.ProductId == productId);
        }
    }
}
