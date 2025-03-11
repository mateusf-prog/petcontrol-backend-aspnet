using Microsoft.AspNetCore.Mvc;
using PetControlSystem.Api.Dto;
using PetControlSystem.Api.Mappers;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;
using System.Net;

namespace PetControlSystem.Api.Controllers
{
    [Route("api/appointments")]
    public class AppointmentsController : MainController
    {
        private readonly IAppointmentService _service;
        private readonly IAppointmentRepository _repository;

        public AppointmentsController(IAppointmentService service,
            IAppointmentRepository repository, 
            INotificator notificator) : base(notificator)
        {
            _service = service;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAll()
        {
            var result = await _repository.GetAlllAppointmentsWithPetSupports();
            return result.Select(o => o.ToDto()).ToList();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<AppointmentDto>> GetById(Guid id)
        {
            var result = await _repository.GetByIdWithPetSupport(id);
            if (result is null) return NotFound();
            return result.ToDto();
        }

        [HttpPost]
        public async Task<ActionResult> Create(AppointmentDto input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _service.Add(input.ToEntity());
            return CustomResponse(HttpStatusCode.Created);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, AppointmentDto input)
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
