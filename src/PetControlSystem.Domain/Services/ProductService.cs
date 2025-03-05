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

        public async Task Add(Product input)
        {
            if (!ExecuteValidation(new ProductValidation(), input)) return;

            if (await _repository.GetById(input.Id) != null)
            {
                Notify("There is already a product with this ID");
                return;
            }

            if (_repository.Get(p => p.Name == input.Name).Result.Any())
            {
                Notify("There is already a product with this name");
                return;
            }

            await _repository.Add(input);
        }

        public async Task Update(Guid id, Product input)
        {
            if (!ExecuteValidation(new ProductValidation(), input)) return;

            var result = await _repository.GetById(input.Id);
            if (result is null)
            {
                Notify("Product not found");
                return;
            }

            result.Update(input.Name, input.Price, input.Stock, input.Description);

            await _repository.Update(result);
        }

        public async Task Delete(Guid id)
        {
            if (await _repository.GetById(id) is null)
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
