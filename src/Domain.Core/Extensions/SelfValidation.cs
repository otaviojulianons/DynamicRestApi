using FluentValidation;
using Common.Notifications;

namespace Domain.Core.Extensions
{
    public static class SelfValidation
    {

        public static bool IsValid<T>(this AbstractValidator<T> validator,T entity, INotificationManager notifications)
        {
            var result = validator.Validate(entity);
            foreach (var item in result.Errors)
                notifications.Errors.Add(new ValidationNotification(item.ErrorMessage, item.PropertyName));
            return result.IsValid;
        }

    }
}
