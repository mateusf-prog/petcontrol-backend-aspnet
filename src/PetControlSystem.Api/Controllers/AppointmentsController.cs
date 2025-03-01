using Microsoft.AspNetCore.Mvc;
using PetControlSystem.Api.Dto;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;

namespace PetControlSystem.Api.Controllers
{
    [Route("api/appointments")]
    public class AppointmentsController : MainController
    {
        private readonly IAppointmentService _service;

        public AppointmentsController(IAppointmentService service, INotificator notificator) : base(notificator)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<AppointmentDto>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> Create(AppointmentDto appointmentDto)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<AppointmentDto>> Update(Guid id, AppointmentDto appointmentDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<AppointmentDto>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
