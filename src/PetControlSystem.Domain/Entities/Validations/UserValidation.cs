using FluentValidation;

namespace PetControlSystem.Domain.Entities.Validations
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("The field {Propertyname} is required")
                .EmailAddress().WithMessage("The field {PropertyName} is invalid");

            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("The field {Propertyname} is required")
                .Length(7, 12).WithMessage("The field {Propertyname} must have between {min} and {max} characters");

            RuleFor(c => c.Document)
                .NotEmpty().WithMessage("The field {Propertyname} is required")
                .Length(11, 14).WithMessage("The field {Propertyname} must have between {min} and {max} characters");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("The field {Propertyname} is required")
                .Length(3, 50).WithMessage("The field {Propertyname} must have between {min} and {max} characters");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("The field {Propertyname} is required")
                .MinimumLength(6).WithMessage("The field {Propertyname} must have more than {ComparisonValue} characters");

            RuleFor(c => c.Type)
                .NotEmpty().WithMessage("The field {Propertyname} is required");

            RuleFor(c => c.DocumentType)
                .NotEmpty().WithMessage("The field {Propertyname} is required");

            RuleFor(c => c.Address)
                .NotEmpty().WithMessage("The field {Propertyname} is required");
        }
    }
}
