using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetControlSystem.Api.Dto;
using PetControlSystem.Api.Mappers;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;
using System.Net;

namespace PetControlSystem.Api.Controllers
{
    [Route("api/users")]
    public class UsersController : MainController
    {
        private readonly IUserService _service;

        public UsersController(IUserService service, INotificator notificator) : base(notificator)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var result = await _service.GetAll();
            return result.Select(x => x.ToDto()).ToList();
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var result = await _service.GetById(id);

            if (result == null) return NotFound();

            return result.ToDto();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Add([FromBody] UserDto input)
        {
            if (ModelState.IsValid) return CustomResponse(ModelState);

            await _service.Register(input.ToEntity());

            return CustomResponse(HttpStatusCode.Created, input);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin, Common")]
        public async Task<ActionResult<UserDto>> Update(Guid id, UserDto input)
        {
            if (ModelState.IsValid) return CustomResponse(ModelState);

            await _service.Update(id, input.ToEntity());

            return CustomResponse(HttpStatusCode.NoContent, input);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto input)
        {
            var result = await _service.Login(input.Email, input.Password);
            return CustomResponse(result);
        }
    }
}
