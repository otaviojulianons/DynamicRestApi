using System.Collections.Generic;
using System.Linq;
using Domain.Core.Interfaces.Structure;
using Common.Notifications;

namespace Tests.Unit.Domain.Utils
{
    public static class ValidationNotificationExtensions
    {
        public static IEnumerable<string> ToPropertiesNameList(this INotificationManager notifications)
        {
            return notifications.Errors
                        .Where( error => error is ValidationNotification)
                        .Select( error => (error as ValidationNotification).Property);
        }
    }
}