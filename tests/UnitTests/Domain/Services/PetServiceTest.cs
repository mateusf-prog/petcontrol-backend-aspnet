using Moq;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;
using PetControlSystem.Domain.Services;
using UnitTests.Fakers;

namespace UnitTests.Domain.Services
{
    public class PetServiceTest
    {
        private readonly PetService _service;
        private readonly Mock<IPetRepository> _petRepository = new();
        private readonly Mock<ICustomerRepository> _customerRepository = new();
        private readonly Notificator _notification = new();

        public PetServiceTest() =>
            _service = new PetService(_petRepository.Object, _customerRepository.Object, _notification);

        [Fact]
        public async Task Add_WhenPetIsValid_ShouldAddPet()
        {
            // Arrange
            var pet = PetFaker.GetValidPet();

            _customerRepository.Setup(c => c.GetById(It.IsAny<Guid>())).ReturnsAsync(new Customer());

            // Act
            await _service.Add(pet);

            // Assert
            _petRepository.Verify(r => r.Add(pet), Times.Once);
        }

        [Fact]
        public async Task Add_WhenCustomerNotFound_ShoulNotify()
        {
            // Arrange
            var pet = PetFaker.GetValidPet();

            _customerRepository.Setup(c => c.GetById(It.IsAny<Guid>())).ReturnsAsync((Customer?) null);

            // Act
            await _service.Add(pet);

            // Assert
            Assert.Contains("Customer not found", _notification.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Add_whenExistsPetWiththeSameId_ShoulNotify()
        {
            // Arrange
            var pet = PetFaker.GetValidPet();

            _customerRepository.Setup(c => c.GetById(It.IsAny<Guid>())).ReturnsAsync(new Customer());
            _petRepository.Setup(r => r.GetById(pet.Id)).ReturnsAsync(pet);

            // Act
            await _service.Add(pet);

            // Assert
            Assert.Contains("There is already a pet with this ID", _notification.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Update_WhenPetIsValid_ShouldUpdatePet()
        {
            // Arrange
            var pet = PetFaker.GetValidPet();

            _petRepository.Setup(r => r.GetById(pet.Id)).ReturnsAsync(pet);
            _customerRepository.Setup(c => c.GetById(It.IsAny<Guid>())).ReturnsAsync(new Customer());

            // Act
            await _service.Update(pet.Id, pet);

            // Assert
            _petRepository.Verify(r => r.Update(pet), Times.Once);
        }

        [Fact]
        public async Task Update_WhenPetNotFound_ShouldNotify()
        {
            // Arrange
            var pet = PetFaker.GetValidPet();

            _petRepository.Setup(r => r.GetById(pet.Id)).ReturnsAsync((Pet?) null);

            // Act
            await _service.Update(pet.Id, pet);

            // Assert
            Assert.Contains("Pet not found", _notification.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Update_WhenCustomerNotFound_ShouldNotify()
        {
            // Arrange
            var pet = PetFaker.GetValidPet();

            _petRepository.Setup(r => r.GetById(pet.Id)).ReturnsAsync(pet);
            _customerRepository.Setup(c => c.GetById(It.IsAny<Guid>())).ReturnsAsync((Customer?) null);

            // Act
            await _service.Update(pet.Id, pet);

            // Assert
            Assert.Contains("Customer not found", _notification.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Delete_WhenPetNotFound_ShouldNotify()
        {
            // Arrange
            var pet = PetFaker.GetValidPet();

            _petRepository.Setup(r => r.GetById(pet.Id)).ReturnsAsync((Pet?) null);

            // Act
            await _service.Delete(pet.Id);

            // Assert
            Assert.Contains("Pet not found", _notification.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Delete_WhenPetExists_ShouldDeletePet()
        {
            // Arrange
            var pet = PetFaker.GetValidPet();

            _petRepository.Setup(r => r.GetById(pet.Id)).ReturnsAsync(pet);

            // Act
            await _service.Delete(pet.Id);

            // Assert
            _petRepository.Verify(r => r.Remove(pet.Id), Times.Once);
        }
    }
}
