using FluentValidation;
using FluentValidation.Results;

namespace EventSourcingCQRS.Domain.Validation
{
    public sealed class AndValidator<T> : AbstractValidator<T>
    {
        readonly AbstractValidator<T> leftValidator;
        readonly AbstractValidator<T> rightValidator;

        public AndValidator(AbstractValidator<T> left, AbstractValidator<T> right)
        {
            leftValidator = left;
            rightValidator = right;
        }

        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var leftResult = leftValidator.Validate(context);
            if (!leftResult.IsValid) return leftResult;

            var rightResult = rightValidator.Validate(context);
            return new ValidationResult(rightResult.Errors);
        }
    }
}