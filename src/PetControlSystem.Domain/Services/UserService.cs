using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;

namespace PetControlSystem.Domain.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository,
                              INotificator notificator) : base(notificator)
        {
            _repository = repository;
        }

        public async Task Add(User user)
        {
            if (!ExecuteValidation(new UserValidation(), user))
                return;

            if (_repository.Get(u => u.Email == user.Email).Result.Any())
            {
                Notify("There is already User with this Email");
                return;
            }

            user.Id = Guid.NewGuid();
            await _repository.Add(user);
        }

        public async Task Delete(Guid id)
        {
            if (_repository.GetById(id) is null)
            {
                Notify("There is no User with this ID");
                return;
            }

            await _repository.Remove(id);
        }

        public async Task GetAll()
        {
            await _repository.GetAll();
        }

        public async Task GetById(Guid id)
        {
            var result = await _repository.GetById(id);

            if (result is null)
            {
                Notify("There is no User with this ID");
                return;
            }

            return;
        }

        public async Task Login(User user)
        {
            var result = await _repository.Get(u => u.Email == user.Email && u.Password == user.Password);

            if (!result.Any())
            {
                Notify("Invalid Email or Password");
                return;
            }

            return;
        }

        public async Task Update(Guid id, User user)
        {
            if (!ExecuteValidation(new UserValidation(), user))
                return;

            if (_repository.GetById(id) is null)
            {
                Notify("There is no User with this ID");
                return;
            }

            await _repository.Update(user);
        }

        public void Dispose()
        {
            _repository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
