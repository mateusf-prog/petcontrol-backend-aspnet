using FluentValidation;

namespace PetControlSystem.Domain.Entities.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation() 
        {
            RuleFor(a => a.Street)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .Length(2, 200).WithMessage("The field {PropertyName} must have between {min} and {max} characters");

            RuleFor(a => a.Number)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .Length(1, 50).WithMessage("The field {PropertyName} must have between {min} and {max} characters");

            RuleFor(a => a.Neighborhood)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .Length(2, 50).WithMessage("The field {PropertyName} must have between {min} and {max} characters");

            RuleFor(a => a.City)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .Length(2, 50).WithMessage("The field {PropertyName} must have between {min} and {max} characters");

            RuleFor(a => a.State)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .Length(2, 50).WithMessage("The field {PropertyName} must have between {min} and {max} characters");

            RuleFor(a => a.PostalCode)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .Length(9).WithMessage("The field {PropertyName} must have {number} characters");
        }
    }
}
