using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace EventSourcingCQRS.Domain.CartModule
{
    public class CartItemInCartValidator : AbstractValidator<CartItem>
    {
        public CartItemInCartValidator(IEnumerable<CartItem> cartItems)
        {
            RuleFor(c => c.ProductId)
                .Must(p => cartItems.Select(c => c.ProductId).Contains(p))
                .WithMessage(c => $"Product {c.ProductId} not found");
        }
    }
}
