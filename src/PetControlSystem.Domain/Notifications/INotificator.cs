namespace PetControlSystem.Domain.Notification
{
    public interface INotificator
    {
        List<Notification> GetNotifications();
        bool HasNotification();
        void Handle(Notification notification);
    }
}
