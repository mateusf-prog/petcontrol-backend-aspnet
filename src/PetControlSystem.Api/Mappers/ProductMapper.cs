using PetControlSystem.Api.Dto;
using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Api.Mappers
{
    public static class ProductMapper
    {
        public static Product RoEntity(this ProductDto productDto)
        {
            return new Product(
                productDto.Name, 
                productDto.Price, 
                productDto.Stock, 
                productDto.Description);
        }

        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description
            };
        }
    }
}
