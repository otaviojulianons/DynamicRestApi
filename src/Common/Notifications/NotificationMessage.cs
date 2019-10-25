namespace Common.Notifications
{
    public class NotificationMessage : INotificationMessage
    {
        public NotificationMessage(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}
