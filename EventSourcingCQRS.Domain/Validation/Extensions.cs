using FluentValidation;

namespace EventSourcingCQRS.Domain.Validation
{
    public static class Extensions
    {
        public static AbstractValidator<T> And<T>(this AbstractValidator<T> leftValidator, AbstractValidator<T> rightValidator)
        {
            return new AndValidator<T>(leftValidator, rightValidator);
        }
    }
}
