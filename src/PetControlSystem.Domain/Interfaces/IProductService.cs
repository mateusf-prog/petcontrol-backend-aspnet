using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IProductService : IDisposable
    {
        Task Add(Product product);
        Task Update(Guid id, Product product);
        Task Delete(Guid id);
        Task<List<Product>> GetProductsByIds(List<Guid> guids);
    }
}
