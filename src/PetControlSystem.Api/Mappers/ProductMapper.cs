using PetControlSystem.Api.Dto;
using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Api.Mappers
{
    public static class ProductMapper
    {
        public static Product ToEntity(this ProductDto dto)
        {
            return new Product(
                dto.Name, 
                dto.Price, 
                dto.Stock, 
                dto.Description);
        }

        public static ProductDto ToDto(this Product entity)
        {
            return new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Stock = entity.Stock,
                Description = entity.Description
            };
        }
    }
}
