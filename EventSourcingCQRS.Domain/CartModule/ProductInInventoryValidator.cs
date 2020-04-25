using System;
using FluentValidation;

namespace EventSourcingCQRS.Domain.CartModule
{
    public class ProductInInventoryValidator : AbstractValidator<CartItem>
    {
        private const int ProductQuantityThreshold = 50;
        public ProductInInventoryValidator()
        {
            RuleFor(c => c.ProductId).Must(p => p.Id != Guid.Empty)
                .WithMessage(c => $"Product {c.ProductId} does not exist");

            RuleFor(c => c.Quantity)
                .Must(q => q > 0)
                .WithMessage(c =>  "Quantity must be greater than zero")
                .Must(q => q <= ProductQuantityThreshold)
                .WithMessage(c => $"Quantity for product {c.ProductId} must be less than or equal to {ProductQuantityThreshold}");
        }
    }
}
