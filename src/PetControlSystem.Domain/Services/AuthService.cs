using Microsoft.AspNetCore.Identity;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;

namespace PetControlSystem.Domain.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthService(SignInManager<IdentityUser> signInManager, 
                            UserManager<IdentityUser> userManager, 
                            ITokenService tokenService, 
                            INotificator notificator) : base(notificator)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                Notify("User not found.");
                return string.Empty;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, true);

            if (!result.Succeeded)
            {
                Notify("Incorrect password.");
                return string.Empty;
            }

            return _tokenService.GenerateToken(user);
        }

        public async Task<string> Register(IdentityUser user)
        {
            var result = await _userManager.CreateAsync(user, user.PasswordHash);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return _tokenService.GenerateToken(user);
            }

            foreach (var error in result.Errors)
            {
                Notify(error.Description);
            }

            return string.Empty;
        }
    }
}