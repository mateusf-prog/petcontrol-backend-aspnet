using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Domain.Interfaces
{
    public interface IProductService : IDisposable
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(Guid id);
        Task<Product> GetById(Guid id);
        Task<IEnumerable<Product>> GetAll();
    }
}
