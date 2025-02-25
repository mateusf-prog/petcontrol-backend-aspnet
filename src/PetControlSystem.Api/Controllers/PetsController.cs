using Microsoft.AspNetCore.Mvc;
using PetControlSystem.Api.Dto;

namespace PetControlSystem.Api.Controllers
{
    [Route("api/pets")]
    public class PetsController : MainController
    {
        public PetsController() { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PetDto>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<PetDto>> Create(PetDto petDto)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PetDto>> Update(Guid id, PetDto petDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<PetDto>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
