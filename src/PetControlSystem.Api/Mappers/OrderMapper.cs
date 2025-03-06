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
                dto.Products.Select(p => p.ToEntity()).ToList(),
                dto.TotalPrice);
        }

        public static OrderDto ToDto(this Order entity)
        {
            return new OrderDto
            {
                Id = entity.Id,
                Date = entity.Date,
                CustomerId = entity.CustomerId,
                Products = entity.OrderProducts.Select(p => p.ToDto()).ToList()
            };
        }
    }
}
