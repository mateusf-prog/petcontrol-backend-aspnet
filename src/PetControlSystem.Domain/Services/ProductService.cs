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

            if (_productRepository.GetById(product.Id) != null)
            {
                Notify("There is already a product with this ID");
                return;
            }

            if (_productRepository.Get(p => p.Name == product.Name).Result.Any())
            {
                Notify("There is already a product with this name");
                return;
            }

            await _productRepository.Add(product);
        }

        public async Task Delete(Guid id)
        {
            if (_productRepository.GetById(id) is null)
            {
                Notify("There is no product with this ID");
                return;
            }

            await _productRepository.Remove(id);
        }

        public async Task<IEnumerable<Product>?> GetAll()
        {
            var result = await _productRepository.GetAll();

            if(result is null)
            {
                Notify("There are no products");
                return null;
            }

            return result;
        }

        public async Task<Product?> GetById(Guid id)
        {
            var result = await _productRepository.GetById(id);

            if (result is null)
            {
                Notify("There are no products");
                return null;
            }

            return result;
        }

        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            if (_productRepository.GetById(product.Id) is null)
            {
                Notify("There is no product with this ID");
                return;
            }

            await _productRepository.Update(product);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
