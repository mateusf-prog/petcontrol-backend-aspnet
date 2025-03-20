using Moq;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;
using PetControlSystem.Domain.Services;
using Xunit;

namespace UnitTests.Domain.Services
{
    public class ProductServiceTest
    {
        private readonly ProductService _service;
        private readonly Mock<IProductRepository> _repositoryMock = new();
        private readonly Notificator _notificator = new();

        public ProductServiceTest()
        {
            _service = new ProductService(_repositoryMock.Object, _notificator);
        }

        [Fact]
        public async Task Add_GivenValidProduct_ShouldAddProduct()
        {
            // Arrange
            var product = new Product("Dog Food", 19.99m, 50, "Premium dog food");

            _repositoryMock.Setup(r => r.GetById(product.Id)).ReturnsAsync((Product?)null);
            _repositoryMock.Setup(r => r.Get(p => p.Name == product.Name)).ReturnsAsync([]);

            // Act
            await _service.Add(product);

            // Assert
            _repositoryMock.Verify(r => r.Add(product), Times.Once);
        }

        [Fact]
        public async Task Add_WhenProductWithSameIdExists_ShouldNotify()
        {
            // Arrange
            var product = new Product("Dog Food", 19.99m, 50, "Premium dog food");

            _repositoryMock.Setup(r => r.GetById(product.Id)).ReturnsAsync(product);

            // Act
            await _service.Add(product);

            // Assert
            Assert.Contains("There is already a product with this ID", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Add_WhenProductWithSameNameExists_ShouldNotify()
        {
            // Arrange
            var product = new Product("Dog Food", 19.99m, 50, "Premium dog food");

            _repositoryMock.Setup(r => r.GetById(product.Id)).ReturnsAsync((Product?)null);
            _repositoryMock.Setup(r => r.Get(p => p.Name == product.Name)).ReturnsAsync([product]);

            // Act
            await _service.Add(product);

            // Assert
            Assert.Contains("There is already a product with this name", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Update_GivenValidProduct_ShouldUpdateProduct()
        {
            // Arrange
            var existingProduct = new Product("Dog Food", 19.99m, 50, "Premium dog food");
            var updatedProduct = new Product("Cat Food", 15.99m, 30, "Premium cat food");

            _repositoryMock.Setup(r => r.GetById(existingProduct.Id)).ReturnsAsync(existingProduct);

            // Act
            await _service.Update(existingProduct.Id, updatedProduct);

            // Assert
            _repositoryMock.Verify(r => r.Update(It.Is<Product>(p =>
                p.Name == updatedProduct.Name &&
                p.Price == updatedProduct.Price &&
                p.Stock == updatedProduct.Stock &&
                p.Description == updatedProduct.Description
            )), Times.Once);
        }

        [Fact]
        public async Task Update_WhenProductNotFound_ShouldNotify()
        {
            // Arrange
            var updatedProduct = new Product("Cat Food", 15.99m, 30, "Premium cat food");

            _repositoryMock.Setup(r => r.GetById(updatedProduct.Id)).ReturnsAsync((Product?)null);

            // Act
            await _service.Update(updatedProduct.Id, updatedProduct);

            // Assert
            Assert.Contains("Product not found", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Delete_WhenProductExists_ShouldDeleteProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product("Dog Food", 19.99m, 50, "Premium dog food");

            _repositoryMock.Setup(r => r.GetById(productId)).ReturnsAsync(product);

            // Act
            await _service.Delete(productId);

            // Assert
            _repositoryMock.Verify(r => r.Remove(productId), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenProductNotFound_ShouldNotify()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _repositoryMock.Setup(r => r.GetById(productId)).ReturnsAsync((Product?)null);

            // Act
            await _service.Delete(productId);

            // Assert
            Assert.Contains("Product not found", _notificator.GetNotifications().Select(n => n.Message));
        }
    }
}
