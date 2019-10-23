using System.Collections.Generic;

namespace Infrastructure.CrossCutting.Notifications
{
    public class NotificationManager : INotificationManager
    {
        public IList<INotificationMessage> Errors { get; set; } = new List<INotificationMessage>();

        public bool HasError => Errors.Count > 0;
    }
}
