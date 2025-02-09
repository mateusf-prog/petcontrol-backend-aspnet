using FluentValidation;
using FluentValidation.Results;
using PetControlSystem.Domain.Notification;

namespace PetControlSystem.Domain.Services
{
    public abstract class BaseService
    {
        private readonly INotificator _notification;

        protected BaseService(INotificator notification)
        {
            _notification = notification;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                Notify(error.ErrorMessage);
        }

        public void Notify(string message)
        {
            _notification.Handle(new Notificator(message));
        }

        protected bool ExecuteValidation<V, E>(V validation, E entity) where V : AbstractValidator<E>
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notify(validator);

            return validator.IsValid;
        }
}
}
