using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;

namespace PetControlSystem.Domain.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Add(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.Add(product);
        }

        public async Task Delete(Guid id)
        {
            await _productRepository.Remove(id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            await _productRepository.GetAll();
            return null;
        }

        public async Task<Product> GetById(Guid id)
        {
            await _productRepository.GetById(id);
            return null;
        }

        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.Update(product);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
