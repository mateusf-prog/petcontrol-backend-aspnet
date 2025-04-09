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
        public async Task Add_GivenInput_ShouldAddAppointment()
        {
            // Arrange
            var pet = new Faker<Pet>().Generate();
            var appointmentPetSupports = new Faker<AppointmentPetSupport>().Generate(2);
            var input = new Faker<Appointment>()
                .RuleFor(a => a.PetId, pet.Id)
                .RuleFor(a => a.CustomerId, Guid.NewGuid)
                .RuleFor(a => a.Date, DateTime.Now.AddDays(1))
                .RuleFor(a => a.AppointmentPetSupports, appointmentPetSupports)
                .Generate();
            var pets = new List<Pet> { pet };
            var customer = new Faker<Customer>().UsePrivateConstructor().RuleFor(c => c.Pets, pets).Generate();
            var petSupports = new Faker<PetSupport>().Generate(2);
            var petSupportsIds = input.AppointmentPetSupports.Select(ps => ps.PetSupportId).ToList();

            _customerRepositoryMock.Setup(c => c.GetCustomerWithPets(input.CustomerId)).ReturnsAsync(customer);
            _petSupportServiceMock.Setup(p => p.GetPetSupportsByIds(petSupportsIds)).ReturnsAsync(petSupports);

            // Act
            await _service.Add(input);

            // Assert 
            _repositoryMock.Verify(r => r.Add(input), Times.Once);
        }

        [Fact]
        public async Task Add_WhenCustomerNotFound_ShouldNotify()
        {
            // Arrange
            var input = AppointmentFaker.GetValidAppointmentFaker();

            _customerRepositoryMock.Setup(c => c.GetCustomerWithPets(input.CustomerId)).ReturnsAsync((Customer?)null);

            // Act
            await _service.Add(input);

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
            var input = AppointmentFaker.GetValidAppointmentFaker();

            _customerRepositoryMock.Setup(c => c.GetCustomerWithPets(input.CustomerId)).ReturnsAsync(customer);
            _petSupportServiceMock.Setup(p => p.GetPetSupportsByIds(It.IsAny<List<Guid>>())).ReturnsAsync([]);

            // Act
            await _service.Add(input);

            // Assert
            Assert.Contains("Pet not found", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Add_WhenPetSupportNotFound_ShouldNotify()
        {
            // Arrange
            var pet = new Faker<Pet>().Generate();
            var customer = new Faker<Customer>()
                .UsePrivateConstructor()
                .RuleFor(c => c.Pets, [pet]).Generate();
            var petSupports = new Faker<PetSupport>().Generate(1);
            var appointmentPetSupports = new Faker<AppointmentPetSupport>().Generate(2);
            var input = new Faker<Appointment>()
                .RuleFor(a => a.PetId, pet.Id)
                .RuleFor(a => a.CustomerId, Guid.NewGuid)
                .RuleFor(a => a.Date, DateTime.Now.AddDays(1))
                .RuleFor(a => a.AppointmentPetSupports, appointmentPetSupports)
                .Generate();

            _customerRepositoryMock.Setup(c => c.GetCustomerWithPets(input.CustomerId)).ReturnsAsync(customer);
            _petSupportServiceMock.Setup(p => p.GetPetSupportsByIds(It.IsAny<List<Guid>>())).ReturnsAsync(petSupports);

            // Act
            await _service.Add(input);

            // Assert
            Assert.Contains("One or more PetSupports not found", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Add_WhenDateIsLessThanNow_ShouldNotify()
        {
            // Arrange
            var appointmentPetSupports = new Faker<AppointmentPetSupport>().Generate(2);
            var input = new Faker<Appointment>()
                .RuleFor(a => a.PetId, Guid.NewGuid)
                .RuleFor(a => a.CustomerId, Guid.NewGuid)
                .RuleFor(a => a.Date, DateTime.Now.AddDays(-1))
                .RuleFor(a => a.AppointmentPetSupports, appointmentPetSupports)
                .Generate();

            // Act
            await _service.Add(input);

            // Assert
            Assert.Contains("The Date date must be greater than now", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Update_GivenInput_ShouldUpdateAppointment()
        {
            // Arrange
            var pet = new Faker<Pet>().Generate();
            var input = AppointmentFaker.GetValidAppointmentFaker();
            var pets = new List<Pet> { pet };
            var customer = new Faker<Customer>().UsePrivateConstructor().RuleFor(c => c.Pets, pets).Generate();
            var petSupports = new Faker<PetSupport>().Generate(2);
            var petSupportsIds = input.AppointmentPetSupports.Select(ps => ps.PetSupportId).ToList();

            _repositoryMock.Setup(r => r.GetByIdWithPetSupport(input.Id)).ReturnsAsync(input);
            _customerRepositoryMock.Setup(c => c.GetCustomerWithPets(input.CustomerId)).ReturnsAsync(customer);
            _petSupportServiceMock.Setup(p => p.GetPetSupportsByIds(petSupportsIds)).ReturnsAsync(petSupports);
            _repositoryMock.Setup(r => r.Update(input));

            // Act
            await _service.Update(input.Id, input);

            // Assert 
            _repositoryMock.Verify(r => r.Update(input), Times.Once);
        }

        [Fact]
        public async Task Update_WhenAppointmentNotFound_ShouldUpdateAppointment()
        {
            // Arrange
            var pet = new Faker<Pet>().Generate();
            var appointmentPetSupports = new Faker<AppointmentPetSupport>().Generate(2);
            var input = new Faker<Appointment>()
                .RuleFor(a => a.PetId, pet.Id)
                .RuleFor(a => a.CustomerId, Guid.NewGuid)
                .RuleFor(a => a.Date, DateTime.Now.AddDays(1))
                .RuleFor(a => a.AppointmentPetSupports, appointmentPetSupports)
                .Generate();
            var pets = new List<Pet> { pet };
            var customer = new Faker<Customer>().UsePrivateConstructor().RuleFor(c => c.Pets, pets).Generate();
            var petSupports = new Faker<PetSupport>().Generate(0);
            var petSupportsIds = input.AppointmentPetSupports.Select(ps => ps.PetSupportId).ToList();

            _repositoryMock.Setup(r => r.GetByIdWithPetSupport(input.Id)).ReturnsAsync((Appointment?)null);
            _customerRepositoryMock.Setup(c => c.GetCustomerWithPets(input.CustomerId)).ReturnsAsync(customer);
            _petSupportServiceMock.Setup(p => p.GetPetSupportsByIds(petSupportsIds)).ReturnsAsync(petSupports);
            _repositoryMock.Setup(r => r.Update(input));

            // Act
            await _service.Update(input.Id, input);

            // Assert 
            Assert.Contains("Appointment not found", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Update_WhenPetSupportNotFound_ShouldNotify()
        {
            // Arrange
            var pet = new Faker<Pet>().Generate();
            var customer = new Faker<Customer>()
                .UsePrivateConstructor()
                .RuleFor(c => c.Pets, [pet]).Generate();
            var petSupports = new Faker<PetSupport>().Generate(0);
            var petSupportsIds = new List<Guid> { Guid.NewGuid() };
            var appointmentPetSupports = new Faker<AppointmentPetSupport>().RuleFor(ps => ps.PetSupportId, petSupportsIds[0]).Generate(1);
            var input = new Faker<Appointment>()
                .RuleFor(a => a.PetId, pet.Id)
                .RuleFor(a => a.CustomerId, customer.Id)
                .RuleFor(a => a.Date, DateTime.Now.AddDays(1))
                .RuleFor(a => a.AppointmentPetSupports, appointmentPetSupports)
                .Generate();

            _repositoryMock.Setup(r => r.GetByIdWithPetSupport(input.Id)).ReturnsAsync(input);
            _customerRepositoryMock.Setup(c => c.GetCustomerWithPets(input.CustomerId)).ReturnsAsync(customer);
            _petSupportServiceMock.Setup(p => p.GetPetSupportsByIds(petSupportsIds)).ReturnsAsync(petSupports);

            // Act
            await _service.Update(input.Id, input);

            // Assert
            Assert.Contains("One or more PetSupports not found", _notificator.GetNotifications().Select(n => n.Message));
        }

        [Fact]
        public async Task Delete_GivenId_ShouldDeleteAppointment()
        {
            // Arrange
            var id = Guid.NewGuid();
            var appointment = new Faker<Appointment>().Generate();

            _repositoryMock.Setup(r => r.GetById(id)).ReturnsAsync(appointment);

            // Act
            await _service.Delete(id);

            // Assert
            _repositoryMock.Verify(r => r.Remove(id), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenAppointmentNotFound_ShouldNotify()
        {
            // Arrange
            var id = Guid.NewGuid();

            _repositoryMock.Setup(r => r.GetById(id)).ReturnsAsync((Appointment?)null);

            // Act
            await _service.Delete(id);

            // Assert
            Assert.Contains("Appointment not found", _notificator.GetNotifications().Select(n => n.Message));
        }
    }
}
