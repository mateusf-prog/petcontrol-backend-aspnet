using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Domain.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Add(User user)
        {
            if(!ExecuteValidation(new UserValidation(), user)) return;

            await _userRepository.Add(user);
        }

        public Task ConfirmCode(string code)
        {
            throw new NotImplementedException();
        }
        public Task Login(User user)
        {
            throw new NotImplementedException();
        }

        public async Task Update(User user)
        {
            if (!ExecuteValidation(new UserValidation(), user)) return;

            await _userRepository.Update(user);
        }

        public async Task UpdatePassword(User user, string newPassword)
        {
            if (!ExecuteValidation(new UserValidation(), user)) return;

            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _userRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
