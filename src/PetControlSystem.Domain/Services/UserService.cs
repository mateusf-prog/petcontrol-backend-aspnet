using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;
using PetControlSystem.Domain.Security;
namespace PetControlSystem.Domain.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository, INotificator notificator) : base(notificator)
        {
            _repository = repository;
        }

        public async Task Register(User user)
        {
            if (!ExecuteValidation(new UserValidation(), user))
                return;

            if (_repository.Get(u => u.Email == user.Email).Result.Any())
            {
                Notify("There is already User with this Email");
                return;
            }

            if (_repository.Get(u => u.Document == user.Document).Result.Any())
            {
                Notify("There is already User with this Document");
                return;
            }

            await _repository.Add(user);
        }

        public async Task Update(Guid id, User input)
        {
            if (!ExecuteValidation(new UserValidation(), input))
                return;

            var result = await _repository.GetById(id);

            if (result is null)
            {
                Notify("User not found");
                return;
            }

            if (_repository.Get(u => u.Document == input.Document && u.Id != id).Result.Any())
            {
                Notify("There is already User with this Document");
                return;
            }

            if (_repository.Get(u => u.Email == input.Email && u.Id != id).Result.Any())
            {
                Notify("There is already User with this Email");
                return;
            }

            result.Update(input.Name!, 
                input.Email!, 
                input.Phone!, 
                input.Document!, 
                input.Password, 
                input.DocumentType, 
                input.Type, 
                input.Address);

            await _repository.Update(result);
        }

        public async Task Delete(Guid id)
        {
            if (await _repository.GetById(id) is null)
            {
                Notify("User not found");
                return;
            }
            await _repository.Remove(id);
        }

        public void Dispose()
        {
            _repository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
