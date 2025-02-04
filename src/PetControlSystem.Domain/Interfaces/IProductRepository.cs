using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task ReturnOldStock(Product product);
    }
}
