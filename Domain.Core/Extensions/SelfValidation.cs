using Infrastructure.CrossCutting.Notifications;
using Domain.Core.Interfaces.Structure;

namespace Domain.Core.Extensions
{
    public static class SelfValidation
    {
        public static bool IsValid<T>(this ISelfValidation<T> validable)
            => validable.Validator.Validate(validable).IsValid;

        public static bool IsValid<T>(this ISelfValidation<T> validable, INotificationManager notifications)
        {
            var result = validable.Validator.Validate(validable);
            foreach (var item in result.Errors)
                notifications.Errors.Add(new ValidationNotification(item.ErrorMessage, item.PropertyName));
            return result.IsValid;
        }

    }
}
