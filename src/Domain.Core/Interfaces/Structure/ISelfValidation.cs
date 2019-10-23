using FluentValidation;
using Common.Notifications;

namespace Domain.Core.Interfaces.Structure
{
    public interface ISelfValidation : IEntity
    {
        bool IsValid(INotificationManager notifications);
    }
}
