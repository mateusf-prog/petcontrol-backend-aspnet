using PetControlSystem.Api.Dto;
using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Api.Mappers
{
    public static class OrderMapper
    {
        public static Order ToEntity(this OrderDto dto)
        {
            return new Order(
                dto.CustomerId,
                dto.Products.Select(p => p.ToEntity()).ToList());
        }

        public static OrderDto ToDto(this Order entity)
        {
            return new OrderDto
            {
                Id = entity.Id,
                Date = entity.Date,
                CustomerId = entity.Customer.Id,
                Products = entity.Products.Select(p => p.ToDto()).ToList()
            };
        }
    }
}
