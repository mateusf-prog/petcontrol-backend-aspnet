using Microsoft.AspNetCore.Mvc;
using PetControlSystem.Api.Dto;

namespace PetControlSystem.Api.Controllers
{
    [Route("api/users")]
    public class UsersController : MainController
    {
        public UsersController() { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UserDto>> Update(Guid id, UserDto userDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<UserDto>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
