using Microsoft.AspNetCore.Mvc;
using PetControlSystem.Api.Dto;

namespace PetControlSystem.Api.Controllers
{
    [Route("api/customers")]
    public class CustomersController : MainController
    {
        public CustomersController() { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CustomerDto>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create(CustomerDto customerDto)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CustomerDto>> Update(Guid id, CustomerDto customerDto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CustomerDto>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
