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
        public async Task<ActionResult> GetAll()
        {
            var users = await _service.GetAll();
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var user = await _service.GetById(id);
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] UserDto input)
        {
            var createdUser = await _service.Add(input.ToEntity());
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser.ToDto());
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin, Common")]
        public async Task<ActionResult> Update(Guid id, UserDto userDto)
        {
            var result = await _service.Update(id, userDto.ToEntity());

            return Ok(result.ToDto());
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await _service.Delete(id);

            if (user == null) return NotFound();

            return Ok(user);
        }
    }
}
