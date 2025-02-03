using FluentValidation;

namespace PetControlSystem.Domain.Entities.Validations
{
    public class PetSupportValidation : AbstractValidator<PetSupport>
    {
        public PetSupportValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .MaximumLength(100).WithMessage("The field {PropertyName} must have a maximum of 100 characters");

            RuleFor(x => x.SmallDogPrice)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .GreaterThan(0).WithMessage("The field {PropertyName} must be greater than {min}");

            RuleFor(x => x.MediumDogPrice)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .GreaterThan(0).WithMessage("The field {PropertyName} must be greater than {min}");

            RuleFor(x => x.LargeDogPrice)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .GreaterThan(0).WithMessage("The field {PropertyName} must be greater than {min}");
        }
    }
}
