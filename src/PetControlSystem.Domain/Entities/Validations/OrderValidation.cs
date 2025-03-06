using FluentValidation;

namespace PetControlSystem.Domain.Entities.Validations
{
    public class OrderValidation : AbstractValidator<Order>
    {
        public OrderValidation()
        {
            RuleFor(o => o.OrderProducts)
                .NotEmpty().WithMessage("The {PropertyName} must have at least one product.")
                .NotNull().WithMessage("The {PropertyName} must have at least one product.");

            RuleFor(o => o.CustomerId)
                .NotEmpty().WithMessage("The field {PropertyName} is required");
        }
    }
}