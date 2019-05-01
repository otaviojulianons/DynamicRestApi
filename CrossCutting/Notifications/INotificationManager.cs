using System.Collections.Generic;

namespace Infrastructure.CrossCutting.Notifications
{
    public interface INotificationManager
    {
        IList<INotificationMessage> Errors { get; set; }

        bool HasError { get;  }
    }
}
