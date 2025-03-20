using System.Collections;
using System.Linq.Expressions;
using Bogus;
using Moq;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;
using PetControlSystem.Domain.Services;

namespace UnitTests.Domain.Services
{
    public class PetSupportServiceTest
    {
        private readonly PetSupportService _service;
        private readonly Mock<IPetSupportRepository> _repositoryMock = new();
        private readonly Notificator _notificator = new();

        public PetSupportServiceTest()
        {
            _service = new PetSupportService(_repositoryMock.Object, _notificator);
        }

        [Fact]
        public async Task Add_GivenValidPetSupport_ShouldAddPetSupport()
        {
            // Arrange
            var petSupport = new PetSupport("Maya", 50.00m, 60.00m, 70.00m, []);

            _repositoryMock.Setup(r => r.GetById(petSupport.Id)).ReturnsAsync((PetSupport)null);
            _repositoryMock.Setup(r => r.Get(It.IsAny<Expression<Func<PetSupport, bool>>>()))
                   .ReturnsAsync((IEnumerable<PetSupport>?)null);
            // Act
            await _service.Add(petSupport);

            // Assert
            _repositoryMock.Verify(r => r.Add(petSupport), Times.Once);
        }

        [Fact]
        public async Task Add_WhenPetSupportWithSameIdExists_ShouldNotify()
        {
            // Arrange
            var petSupport = new PetSupport("Maya", 50.00m, 60.00m, 70.00m, []);

            _repositoryMock.Setup(r => r.GetById(petSupport.Id)).ReturnsAsync(petSupport);

            // Act
            await _service.Add(petSupport);

            // Assert
            Assert.Contains("There is already a pet with this ID", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Add_WhenPetSupportWithSameNameExists_ShouldNotify()
        {
            // Arrange
            var petSupport = new PetSupport("Maya", 50.00m, 60.00m, 70.00m, []);

            _repositoryMock.Setup(r => r.GetById(petSupport.Id)).ReturnsAsync((PetSupport)null);
            _repositoryMock.Setup(r => r.Get(ps => ps.Name == petSupport.Name)).ReturnsAsync([petSupport]);

            // Act
            await _service.Add(petSupport);

            // Assert
            Assert.Contains("There is already a pet with this name", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Update_GivenValidPetSupport_ShouldUpdatePetSupport()
        {
            // Arrange
            var input = new PetSupport("Maya", 50.00m, 60.00m, 70.00m, []);
            var existingPetSupport = new Faker<PetSupport>().Generate();

            _repositoryMock.Setup(r => r.GetById(input.Id)).ReturnsAsync(existingPetSupport);

            // Act
            await _service.Update(input.Id, input);

            // Assert
            _repositoryMock.Verify(r => r.Update(existingPetSupport), Times.Once);
        }

        [Fact]
        public async Task Update_WhenPetSupportNotFound_ShouldNotify()
        {
            // Arrange
            var input = new PetSupport("Maya", 50.00m, 60.00m, 70.00m, []);

            _repositoryMock.Setup(r => r.GetById(input.Id)).ReturnsAsync((PetSupport)null);

            // Act
            await _service.Update(input.Id, input);

            // Assert
            Assert.Contains("PetSupport not found", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Delete_WhenPetSupportExists_ShouldDeletePetSupport()
        {
            // Arrange
            var id = Guid.NewGuid();
            var petSupport = new Faker<PetSupport>().Generate();

            _repositoryMock.Setup(r => r.GetById(id)).ReturnsAsync(petSupport);

            // Act
            await _service.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Remove(id), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenPetSupportNotFound_ShouldNotify()
        {
            // Arrange
            var id = Guid.NewGuid();

            _repositoryMock.Setup(r => r.GetById(id)).ReturnsAsync((PetSupport)null);

            // Act
            await _service.Delete(id);

            // Assert
            Assert.Contains("PetSupport not found", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task GetPetSupportsByIds_WhenAllIdsAreValid_ShouldReturnPetSupports()
        {
            // Arrange
            var petSupport1 = new Faker<PetSupport>().Generate();
            var petSupport2 = new Faker<PetSupport>().Generate();
            var ids = new List<Guid> { petSupport1.Id, petSupport2.Id };

            _repositoryMock.Setup(r => r.GetById(petSupport1.Id)).ReturnsAsync(petSupport1);
            _repositoryMock.Setup(r => r.GetById(petSupport2.Id)).ReturnsAsync(petSupport2);

            // Act
            var result = await _service.GetPetSupportsByIds(ids);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(petSupport1, result);
            Assert.Contains(petSupport2, result);
        }

        [Fact]
        public async Task GetPetSupportsByIds_WhenAnyIdIsInvalid_ShouldNotifyAndReturnPartialList()
        {
            // Arrange
            var petSupport1 = new Faker<PetSupport>().Generate();
            var invalidId = Guid.NewGuid();
            var ids = new List<Guid> { petSupport1.Id, invalidId };

            _repositoryMock.Setup(r => r.GetById(petSupport1.Id)).ReturnsAsync(petSupport1);
            _repositoryMock.Setup(r => r.GetById(invalidId)).ReturnsAsync((PetSupport)null);

            // Act
            var result = await _service.GetPetSupportsByIds(ids);

            // Assert
            Assert.Single(result);
            Assert.Contains(petSupport1, result);
            Assert.Contains($"Service not found - ID {invalidId}", _notificator.GetNotifications().Select(n => n.Message));
        }
    }
}
