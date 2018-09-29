using Domain.Interfaces.Structure;
using SharedKernel.Notifications;

namespace Domain.Helpers.Extensions
{
    public static class SelfValidation
    {
        public static bool IsValid<T>(this ISelfValidation<T> validable)
            => validable.Validator.Validate(validable).IsValid;

        public static bool IsValid<T>(this ISelfValidation<T> validable, IMsgManager notifications)
        {
            var result = validable.Validator.Validate(validable);
            foreach (var item in result.Errors)
                notifications.Errors.Add(new MsgValidation(item.ErrorMessage, item.PropertyName));
            return result.IsValid;
        }
            
    }
}
