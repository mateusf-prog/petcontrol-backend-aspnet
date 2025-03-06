using PetControlSystem.Api.Dto;
using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Api.Mappers
{
    public static class OrderProductMapper
    {
        public static OrderProduct ToEntity(this OrderProductDto dto)
        {
            return new OrderProduct
            {
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                Price = dto.Price
            };
        }

        public static OrderProductDto ToDto(this OrderProduct entity)
        {
            return new OrderProductDto
            {
                ProductId = entity.ProductId,
                Quantity = entity.Quantity,
                Price = entity.Price
,            };
        }
    }
}
