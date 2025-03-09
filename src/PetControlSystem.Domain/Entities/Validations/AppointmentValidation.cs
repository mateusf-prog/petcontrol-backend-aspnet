using FluentValidation;

namespace PetControlSystem.Domain.Entities.Validations
{
    public class AppointmentValidation : AbstractValidator<Appointment>
    {
        public AppointmentValidation() 
        {
            RuleFor(a => a.Date)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .GreaterThan(DateTime.Now).WithMessage("The {PropertyName} date must be greater than now");
        }
    }
}
