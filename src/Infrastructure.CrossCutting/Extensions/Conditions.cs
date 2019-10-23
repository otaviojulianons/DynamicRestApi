using System;

namespace Infrastructure.CrossCutting.Extensions
{
    public static class Conditions
    {
        public static (T Data, bool ConditionResult) If<T>(this T @object, Predicate<T> predicate)
        {
            return (@object, predicate(@object));
        }

        public static (T Data, bool ConditionResult) If<T>(this (T Data, bool ConditionResult) conditional, Predicate<T> predicate) 
        {
            return (conditional.Data, predicate(conditional.Data));
        }

        public static (T Data, bool conditionResult) IfNotNull<T>(this T @object) where T : class
        {
            return (@object, @object != null);
        }

        public static (T Data, bool conditionResult) Then<T>(this (T Data, bool ConditionResult) conditional, Action<T> action)
        {
            if (conditional.ConditionResult)
                action(conditional.Data);

            return conditional;
        }

        public static (T Data, bool conditionResult) Else<T>(this (T Data, bool ConditionResult) conditional, Action<T> action) 
            => Then((conditional.Data, !conditional.ConditionResult), action);
    }

}
