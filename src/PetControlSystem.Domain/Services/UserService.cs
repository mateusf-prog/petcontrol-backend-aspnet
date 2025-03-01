using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;
using PetControlSystem.Domain.Security;
using PetControlSystem.Domain.Utils;
namespace PetControlSystem.Domain.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _repository;
        private readonly TokenService _tokenService;

        public UserService(IUserRepository repository, TokenService tokenService, INotificator notificator) : base(notificator)
        {
            _repository = repository;
            _tokenService = tokenService;
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

            await _repository.Add(user);
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

        public async Task<dynamic> Login(string email, string password)
        {
            var result = await _repository.Get(u => u.Email == email);

            if (!result.Any())
            {
                Notify("User not found");
                return null;
            }

            if (PasswordUtils.ValidatePassword(result.FirstOrDefault()!.Password!, password) is false)
            {
                Notify("Invalid Password");
                return null;
            }

            var token = _tokenService.GenerateToken(result.FirstOrDefault()!);

            return new
            {
                user = result.FirstOrDefault(),
                token
            };
        }

        public async Task Update(Guid id, User user)
        {
            if (!ExecuteValidation(new UserValidation(), user))
                return;

            if (_repository.Get(u => u.Document == user.Document && u.Id != id).Result.Any())
            {
                Notify("There is already User with this Document");
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
