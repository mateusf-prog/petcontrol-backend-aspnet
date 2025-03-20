using Moq;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;
using PetControlSystem.Domain.Services;
using UnitTests.Fakers;

namespace UnitTests.Domain.Services
{
    public class OrderServiceTest
    {
        private readonly OrderService _service;
        private readonly Mock<IOrderRepository> _repositoryMock = new();
        private readonly Mock<IProductRepository> _productRepositoryMock = new();
        private readonly Mock<ICustomerRepository> _customerRepositoryMock = new();
        private readonly Notificator _notificator = new();

        public OrderServiceTest()
        {
            _service = new OrderService(
                _repositoryMock.Object,
                _notificator,
                _productRepositoryMock.Object,
                _customerRepositoryMock.Object
            );
        }

        [Fact]
        public async Task Add_GivenValidOrder_ShouldAddOrder()
        {
            // Arrange
            var input = OrderFaker.GetValidOrderFaker();
            var product = new Product("Food", 100, 10, "Description");

            _repositoryMock.Setup(r => r.GetById(input.Id)).ReturnsAsync((Order?)null);
            _customerRepositoryMock.Setup(r => r.GetById(input.CustomerId)).ReturnsAsync(new Customer());
            _productRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync(product);

            // Act
            await _service.Add(input);

            // Assert
            _repositoryMock.Verify(r => r.Add(input), Times.AtLeastOnce);
            _productRepositoryMock.Verify(r => r.Update(It.IsAny<Product>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task Add_WhenOrderWithSameIdExists_ShouldNotify()
        {
            // Arrange
            var input = OrderFaker.GetValidOrderFaker();

            _repositoryMock.Setup(r => r.GetById(input.Id)).ReturnsAsync(new Order());
            _customerRepositoryMock.Setup(r => r.GetById(input.CustomerId)).ReturnsAsync(new Customer());
            _productRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync(new Product());

            // Act
            await _service.Add(input);

            // Assert
            Assert.Contains("There is already an order with this ID", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Add_WhenCustomerNotFound_ShouldNotify()
        {
            // Arrange
            var input = OrderFaker.GetValidOrderFaker();

            _repositoryMock.Setup(r => r.GetById(input.Id)).ReturnsAsync((Order?)null);
            _customerRepositoryMock.Setup(r => r.GetById(input.CustomerId)).ReturnsAsync((Customer?)null);

            // Act
            await _service.Add(input);

            // Assert
            Assert.Contains($"Customer not found - ID {input.CustomerId}", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Add_WhenProductNotFound_ShouldNotify()
        {
            // Arrange
            var input = OrderFaker.GetValidOrderFaker();

            _repositoryMock.Setup(r => r.GetById(input.Id)).ReturnsAsync((Order?)null);
            _customerRepositoryMock.Setup(r => r.GetById(input.CustomerId)).ReturnsAsync(new Customer());
            _productRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync((Product?)null);

            // Act
            await _service.Add(input);

            // Assert
            Assert.Contains($"Product not found - ID {input.OrderProducts[0].ProductId}", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Add_WhenDoesNotHaveSufficientStock_ShouldNotify()
        {
            // Arrange
            var input = OrderFaker.GetValidOrderFaker();
            var product = new Product("Food", 100, 0, "Description");

            _repositoryMock.Setup(r => r.GetById(input.Id)).ReturnsAsync((Order?)null);
            _customerRepositoryMock.Setup(r => r.GetById(input.CustomerId)).ReturnsAsync(new Customer());
            _productRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync(product);

            // Act
            await _service.Add(input);

            // Assert
            Assert.Contains($"Insufficient stock of product - {input.OrderProducts[0].ProductId}", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Update_GivenValidOrder_ShouldUpdateOrder()
        {
            // Arrange
            var id = Guid.NewGuid();
            var input = OrderFaker.GetValidOrderFaker();

            _repositoryMock.Setup(r => r.GetById(id)).ReturnsAsync(new Order());

            // Act
            await _service.Update(id, input);

            // Assert
            _repositoryMock.Verify(r => r.Update(It.IsAny<Order>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task Update_WhenOrderNotFound_ShouldNotify()
        {
            // Arrange
            var id = Guid.NewGuid();
            var input = OrderFaker.GetValidOrderFaker();

            _repositoryMock.Setup(r => r.GetById(id)).ReturnsAsync((Order?)null);

            // Act
            await _service.Update(id, input);

            // Assert
            Assert.Contains("Order not found", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Delete_GivenValidId_ShouldDeleteOrder()
        {
            // Arrange
            var id = Guid.NewGuid();

            _repositoryMock.Setup(r => r.GetByIdWithProducts(id)).ReturnsAsync(new Order());
            _productRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync(new Product());

            // Act
            await _service.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Remove(It.IsAny<Guid>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task Delete_WhenOrderNotFound_ShouldNotify()
        {
            // Arrange
            var id = Guid.NewGuid();

            _repositoryMock.Setup(r => r.GetByIdWithProducts(id)).ReturnsAsync((Order?)null);

            // Act
            await _service.Delete(id);

            // Assert
            Assert.Contains("Order not found", _notificator.GetNotifications().Select(n => n.Message));
        }
    }
}