using Microsoft.AspNetCore.Mvc;
using PetControlSystem.Api.Dto;
using PetControlSystem.Api.Mappers;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;
using System.Net;

namespace PetControlSystem.Api.Controllers
{
    [Route("api/addresses")]
    public class AddressesController : MainController
    {
        private readonly IAddressService _service;
        private readonly IAddressRepository _repository;

        public AddressesController(IAddressService service,IAddressRepository repository, INotificator notificator) : base(notificator)
        {
            _service = service;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAll()
        {
            var result = await _repository.GetAll();
            return result.Select(a => a.ToDto()).ToList();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<AddressDto>> GetById(Guid id)
        {
            var result = await _repository.GetById(id);
            if (result is null) return NotFound();
            return result.ToDto();
        }

        [HttpPost]
        public async Task<ActionResult<AddressDto>> Create(AddressDto addressDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _service.Add(addressDto.ToEntity());
            return CustomResponse(HttpStatusCode.Created, addressDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<AddressDto>> Update(Guid id, AddressDto input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _service.Update(id, input.ToEntity());
            return CustomResponse(HttpStatusCode.NoContent, input);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<AddressDto>> Delete(Guid id)
        {
            await _service.Delete(id);
            return CustomResponse(HttpStatusCode.NoContent);
        }
    }
}
