using Bogus;
using Moq;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;
using PetControlSystem.Domain.Services;
using UnitTests.Fakers;

namespace UnitTests.Domain.Services
{
    public class CustomerServiceTest
    {
        private readonly CustomerService _service;
        private readonly Mock<ICustomerRepository> _repositoryMock = new();
        private readonly Notificator _notificator = new();

        public CustomerServiceTest()
        {
            _service = new CustomerService(_repositoryMock.Object, _notificator);
        }

        [Fact]
        public async Task Add_GivenValidCustomer_ShouldAddCustomer()
        {
            // Arrange
            var input = CustomerFaker.GetValidCustomerFaker();

            _repositoryMock.Setup(r => r.Get(c => c.Document == input.Document)).ReturnsAsync(Enumerable.Empty<Customer>());
            _repositoryMock.Setup(r => r.Get(c => c.Email == input.Email)).ReturnsAsync(Enumerable.Empty<Customer>());

            // Act
            await _service.Add(input);

            // Assert
            _repositoryMock.Verify(r => r.Add(input), Times.Once);
        }

        [Fact]
        public async Task Add_WhenCustomerWithSameDocumentExists_ShouldNotify()
        {
            // Arrange
            var input = CustomerFaker.GetValidCustomerFaker();

            _repositoryMock.Setup(r => r.Get(c => c.Document == input.Document)).ReturnsAsync([input]);

            // Act
            await _service.Add(input);

            // Assert
            Assert.Contains("There is already a customer with this document", _notificator.GetNotifications().Select(n => n.Message));
        }

                [Fact]
        public async Task Add_WhenCustomerWithSameEmailExists_ShouldNotify()
        {
            // Arrange
            var input = CustomerFaker.GetValidCustomerFaker();

            _repositoryMock.Setup(r => r.Get(c => c.Email == input.Email)).ReturnsAsync([input]);

            // Act
            await _service.Add(input);

            // Assert
            Assert.Contains("There is already a customer with this email", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Update_GivenValidCustomer_ShouldUpdateCustomer()
        {
            // Arrange
            var input = CustomerFaker.GetValidCustomerFaker();
            var result = CustomerFaker.GetValidCustomerFaker();

            _repositoryMock.Setup(r => r.GetById(input.Id)).ReturnsAsync(result);

            // Act
            await _service.Update(input.Id, input);

            // Assert
            _repositoryMock.Verify(r => r.Update(result), Times.Once);
        }

        [Fact]
        public async Task Update_WhenCustomerNotFound_ShouldNotify()
        {
            // Arrange
            var input = CustomerFaker.GetValidCustomerFaker();

            _repositoryMock.Setup(r => r.GetById(input.Id)).ReturnsAsync((Customer?)null);

            // Act
            await _service.Update(input.Id, input);

            // Assert
            Assert.Contains("Customer not found", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Delete_GivenValidId_ShouldDeleteCustomer()
        {
            // Arrange
            var input = CustomerFaker.GetValidCustomerFaker();

            _repositoryMock.Setup(r => r.GetById(input.Id)).ReturnsAsync(input);

            // Act
            await _service.Delete(input.Id);

            // Assert
            _repositoryMock.Verify(r => r.Remove(input.Id), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenCustomerNotFound_ShouldNotify()
        {
            // Arrange
            var input = CustomerFaker.GetValidCustomerFaker();

            _repositoryMock.Setup(r => r.GetById(input.Id)).ReturnsAsync((Customer)null);

            // Act
            await _service.Delete(input.Id);

            // Assert
            Assert.Contains("Customer not found", _notificator.GetNotifications().Select(n => n.Message));
        }
    }
}

