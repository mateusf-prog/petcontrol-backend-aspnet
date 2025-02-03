namespace PetControlSystem.Domain.Interfaces
{
    public interface IHistoriesService : IDisposable
    {
        Task GetOrders();
        Task GetAppointments();
    }
}
