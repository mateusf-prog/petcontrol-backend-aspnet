using Microsoft.AspNetCore.Mvc;
using PetControlSystem.Api.Dto;
using PetControlSystem.Api.Mappers;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;
using System.Net;

namespace PetControlSystem.Api.Controllers
{
    [Route("api/orders")]
    public class OrdersController : MainController
    {
        private readonly IOrderService _service;
        private readonly IOrderRepository _repository;

        public OrdersController(IOrderService service,IOrderRepository repository, INotificator notificator) : base(notificator)
        {
            _service = service;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            var result = await _repository.GetAll();
            return result.Select(o => o.ToDto()).ToList();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderDto>> GetById(Guid id)
        {
            var result = await _repository.GetById(id);
            if (result is null) return NotFound();
            return result.ToDto();
        }

        [HttpPost]
        public async Task<ActionResult> Create(OrderDto input)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _service.Add(input.ToEntity());
            return CustomResponse(HttpStatusCode.Created);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, OrderDto input)
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
