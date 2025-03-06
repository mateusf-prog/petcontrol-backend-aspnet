using FluentValidation;

namespace PetControlSystem.Domain.Entities.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .Length(3, 50).WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characters");

            RuleFor(p => p.Stock)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .GreaterThan(0).WithMessage("The field {PropertyName} must be greater than {ComparisonValue}");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .GreaterThan(0).WithMessage("The field {PropertyName} must be greater than {ComparisonValue}");
        }
    }
}
