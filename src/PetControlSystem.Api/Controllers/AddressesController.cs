using Microsoft.AspNetCore.Mvc;
using PetControlSystem.Api.Dto;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;

namespace PetControlSystem.Api.Controllers
{
    [Route("api/addresses")]
    public class AddressesController : MainController
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService service, INotificator notificator) : base(notificator)
        {
            _addressService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<AddressDto>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<AddressDto>> Create(AddressDto addressDto)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<AddressDto>> Update(Guid id, AddressDto addressDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<AddressDto>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
