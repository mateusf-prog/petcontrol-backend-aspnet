using PetControlSystem.Domain.Interfaces;
using PetControlSystem.Domain.Notifications;

namespace PetControlSystem.Domain.Services
{
    public class HistoriesService : BaseService, IHistoriesService
    {
        private readonly IOrderService _orderService;
        private readonly IAppointmentService _appointmentService;

        public HistoriesService(IOrderService orderService,
                                IAppointmentService appointmentService,
                                INotificator notificator) : base(notificator)
        {
            _orderService = orderService;
            _appointmentService = appointmentService;
        }

        public async Task GetAppointments()
        {
            await _appointmentService.GetAll();
        }

        public async Task GetOrders()
        {
            await _orderService.GetAll();
        }

        public void Dispose()
        {
            _orderService?.Dispose();
            _appointmentService?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
