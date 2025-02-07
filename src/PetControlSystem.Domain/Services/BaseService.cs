using FluentValidation;

namespace PetControlSystem.Domain.Services
{
    public abstract class BaseService
    {

        public void Notify(string message)
        {
        }

        protected bool ExecuteValidation<V, E>(V validation, E entity) where V : AbstractValidator<E>
        {
            var result = validation.Validate(entity);
            return result.IsValid;
        }
}
}
