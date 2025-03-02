using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Entities.Validations;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;

namespace PetControlSystem.Domain.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository, 
                              INotificator notificator) : base(notificator)
        {
            _repository = repository;
        }

        public async Task Add(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            if (_repository.GetById(product.Id) != null)
            {
                Notify("There is already a product with this ID");
                return;
            }

            if (_repository.Get(p => p.Name == product.Name).Result.Any())
            {
                Notify("There is already a product with this name");
                return;
            }

            await _repository.Add(product);
        }

        public async Task Update(Guid id, Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            if (_repository.GetById(product.Id) is null)
            {
                Notify("Product not found");
                return;
            }

            await _repository.Update(product);
        }

        public async Task Delete(Guid id)
        {
            if (_repository.GetById(id) is null)
            {
                Notify("Product not found");
                return;
            }

            await _repository.Remove(id);
        }

        public void Dispose()
        {
            _repository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
