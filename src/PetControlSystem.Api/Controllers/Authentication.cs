using Microsoft.AspNetCore.Mvc;
using PetControlSystem.Api.Dto;
using PetControlSystem.Api.Mappers;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Api.Controllers
{
    [Route("api/login")]
    public class Authentication : MainController
    {
        private readonly IUserService _userService;

        public Authentication(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<ActionResult<UserDto>> Authenticate([FromBody]UserDto user)
        {
            var result = await _userService.Login(user.ToEntity());

            return Ok(result);
        }
    }
}
