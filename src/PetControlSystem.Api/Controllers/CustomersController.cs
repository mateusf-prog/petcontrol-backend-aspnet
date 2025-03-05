using Microsoft.AspNetCore.Mvc;
using PetControlSystem.Api.Dto;
using PetControlSystem.Api.Mappers;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;
using System.Net;

namespace PetControlSystem.Api.Controllers
{
    [Route("api/customers")]
    public class CustomersController : MainController
    {
        private readonly ICustomerService _service;
        private readonly ICustomerRepository _repository;

        public CustomersController(ICustomerService service,ICustomerRepository repository, INotificator notificator) : base(notificator)
        {
            _service = service;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            var result = await _repository.GetAll();
            return result.Select(c => c.ToDto()).ToList();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CustomerDto>> GetById(Guid id)
        {
            var result = await _repository.GetById(id);
            if (result is null) return NotFound();
            return result.ToDto();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CustomerDto input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _service.Add(input.ToEntity());
            return CustomResponse(HttpStatusCode.Created);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, CustomerDto input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _service.Update(id, input.ToEntity());
            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return CustomResponse(HttpStatusCode.NoContent);
        }
    }
}
