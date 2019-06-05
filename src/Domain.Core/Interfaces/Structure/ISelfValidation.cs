using FluentValidation;
using Infrastructure.CrossCutting.Notifications;

namespace Domain.Core.Interfaces.Structure
{
    public interface ISelfValidation : IEntity
    {
        bool IsValid(INotificationManager notifications);
    }
}
