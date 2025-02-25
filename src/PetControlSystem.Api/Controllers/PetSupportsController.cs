using Microsoft.AspNetCore.Mvc;
using PetControlSystem.Api.Dto;

namespace PetControlSystem.Api.Controllers
{
    [Route("api/pet-supports")]
    public class PetSupportsController : MainController
    {
        public PetSupportsController() { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetSupportDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PetSupportDto>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<PetSupportDto>> Create(PetSupportDto petSupportDto)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PetSupportDto>> Update(Guid id, PetSupportDto petSupportDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<PetSupportDto>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
