using FluentValidation;

namespace PetControlSystem.Domain.Entities.Validations
{
    public class PetValidation : AbstractValidator<Pet>
    {
        public PetValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .Length(2, 30).WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");

            RuleFor(p => p.Weight)
                .NotEmpty().WithMessage("The {PropertyName} field is required")
                .GreaterThan(0).WithMessage("The {PropertyName} field must be greater than {ComparisonValue}");

            RuleFor(p => p.CustomerId)
                .NotEmpty().WithMessage("The {PropertyName} field is required");
        }
    }
}