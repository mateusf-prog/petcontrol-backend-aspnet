namespace PetControlSystem.Domain.Notification
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
