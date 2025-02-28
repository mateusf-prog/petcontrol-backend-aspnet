using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetControlSystem.Api.Dto;
using PetControlSystem.Api.Mappers;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Api.Controllers
{
    [Route("api/users")]
    public class UsersController : MainController
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            await _service.GetAll();
            return Ok();
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            await _service.GetById(id);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Create([FromBody] UserDto input)
        {
            await _service.Add(input.ToEntity());
            return Ok();
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin, Common")]
        public async Task<ActionResult<UserDto>> Update(Guid id, UserDto userDto)
        {
            await _service.Update(id, userDto.ToEntity());
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
