using System.Net;
using Microsoft.AspNetCore.Mvc;
using PetControlSystem.Api.Dto;
using PetControlSystem.Api.Mappers;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;

namespace PetControlSystem.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController : MainController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, INotificator notificator) : base(notificator)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(UserDto input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _authService.Register(input.ToEntity());
            return CustomResponse(HttpStatusCode.OK, result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _authService.Login(input.Email, input.Password);
            return CustomResponse(HttpStatusCode.OK, result);
        }
    }
}