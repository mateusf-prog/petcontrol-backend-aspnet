namespace PetControlSystem.Domain.Notifications
{
    public interface INotificator
    {
        List<Notification> GetNotifications();
        bool HasNotification();
        void Handle(Notification notification);
    }
}
