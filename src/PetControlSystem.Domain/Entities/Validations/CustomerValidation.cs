using FluentValidation;

namespace PetControlSystem.Domain.Entities.Validations
{
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .EmailAddress().WithMessage("The field {PropertyName} is invalid");

            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .Length(7, 12).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Document)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .Length(11, 14).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characters");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .Length(3, 50).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characters");
        }
    }
}