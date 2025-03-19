using Bogus;
using Moq;
using PetControlSystem.Domain.Entities;
using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;
using PetControlSystem.Domain.Services;
using UnitTests.Fakers;

namespace UnitTests.Domain.Services
{
    public class AppointmentServiceTest
    {
        private readonly AppointmentService _service;
        private readonly Notificator _notificator = new();
        private readonly Mock<ICustomerRepository> _customerRepositoryMock = new();
        private readonly Mock<IPetSupportService> _petSupportServiceMock = new();
        private readonly Mock<IAppointmentRepository> _repositoryMock = new();

        public AppointmentServiceTest()
        {
            _service = new AppointmentService(
                _repositoryMock.Object,
                _notificator,
                _customerRepositoryMock.Object,
                _petSupportServiceMock.Object
            );
        }

        [Fact]
        public async Task Add_GivenValidInput_ShouldAddAppointment()
        {
            // Arrange
            var pet = new Faker<Pet>().Generate();
            var validInput = new Faker<Appointment>()
                .RuleFor(a => a.PetId, pet.Id)
                .RuleFor(a => a.Date, DateTime.Now.AddDays(1))
                .RuleFor(a => a.AppointmentPetSupports, [])
                .Generate();
            var pets = new List<Pet> { pet };
            var customer = new Faker<Customer>().UsePrivateConstructor().RuleFor(c => c.Pets, pets).Generate();
            var petSupports = new Faker<PetSupport>().Generate(0);
            var petSupportsIds = validInput.AppointmentPetSupports.Select(ps => ps.PetSupportId).ToList();

            _customerRepositoryMock.Setup(c => c.GetCustomerWithPets(validInput.CustomerId)).ReturnsAsync(customer);
            _petSupportServiceMock.Setup(p => p.GetPetSupportsByIds(petSupportsIds)).ReturnsAsync(petSupports);
            _repositoryMock.Setup(r => r.Add(validInput));

            // Act
            await _service.Add(validInput);

            // Assert 
            _repositoryMock.Verify(r => r.Add(validInput), Times.Once);
        }

        [Fact]
        public async Task Add_WhenCustomerNotFound_ShouldNotify()
        {
            // Arrange
            var invalidInput = new Faker<Appointment>()
                .RuleFor(a => a.Date, DateTime.Now.AddDays(1))
                .Generate();

            _customerRepositoryMock.Setup(c => c.GetCustomerWithPets(invalidInput.CustomerId)).ReturnsAsync((Customer)null);

            // Act
            await _service.Add(invalidInput);

            // Assert
            Assert.Contains("Customer not found", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Add_WhenPetNotFound_ShouldNotify()
        {
            // Arrange
            var pet = new Faker<Pet>().Generate();
            var customer = new Faker<Customer>()
                .UsePrivateConstructor()
                .RuleFor(c => c.Pets, [pet]).Generate();
            var invalidInput = new Faker<Appointment>()
                .RuleFor(a => a.Date, DateTime.Now.AddDays(1))
                .RuleFor(a => a.PetId, Guid.NewGuid())
                .Generate();

            _customerRepositoryMock.Setup(c => c.GetCustomerWithPets(invalidInput.CustomerId)).ReturnsAsync(customer);
            _petSupportServiceMock.Setup(p => p.GetPetSupportsByIds(It.IsAny<List<Guid>>())).ReturnsAsync([]);

            // Act
            await _service.Add(invalidInput);

            // Assert
            Assert.Contains("Pet not found", _notificator.GetNotifications().Select(n => n.Message));
        }
    }
}
