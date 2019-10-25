using System.Collections.Generic;

namespace Common.Notifications
{
    public interface INotificationManager
    {
        IList<INotificationMessage> Errors { get; set; }

        bool HasError { get;  }
    }
}
