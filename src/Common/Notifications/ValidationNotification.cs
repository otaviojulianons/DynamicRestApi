namespace Common.Notifications
{
    public class ValidationNotification : INotificationMessage
    {
        public ValidationNotification(string message, string property)
        {
            Message = message;
            Property = property;
        }

        public string Message { get; private set; }

        public string Property { get; private set; }
    }
}
