namespace PetControlSystem.Domain.Notifications
{
    public class Notification
    {
        public string? _message { get; }

        public Notification(string message)
        {
            _message = message;
        }
    }
}
