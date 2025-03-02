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
        private readonly IUserRepository _repository;

        public UsersController(IUserService service, IUserRepository repository, INotificator notificator) : base(notificator)
        {
            _service = service;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var result = await _repository.GetAll();
            return result.Select(u => u.ToDto()).ToList();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var result = await _repository.GetById(id);
            if (result is null) return NotFound();
            return result.ToDto();
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] UserDto input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _service.Register(input.ToEntity());
            return CustomResponse(HttpStatusCode.Created, input);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, UserDto input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _service.Update(id, input.ToEntity());
            return CustomResponse(HttpStatusCode.NoContent, input);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return CustomResponse(HttpStatusCode.NoContent);
        }
    }
}
