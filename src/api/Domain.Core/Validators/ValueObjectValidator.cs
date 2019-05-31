using Domain.Core.Interfaces.Structure;
using FluentValidation;

namespace Domain.Core.Validators
{
    public static class ValueObjectValidator
    {
        public static IRuleBuilderOptions<T, IValueObject<V>> Valid<T,V>(this IRuleBuilder<T, IValueObject<V>> ruleBuilder)
        {
            return ruleBuilder.Must(ValidValueObject);
        }

        public static bool ValidValueObject<T>(IValueObject<T> name) => name?.Validate(name).IsValid ?? false;
    }
}
