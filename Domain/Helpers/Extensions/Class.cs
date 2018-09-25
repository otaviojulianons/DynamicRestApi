using System;

namespace Domain.Helpers.Extensions
{
    static class Class
    {
        public static void Set<T>(this T @object, Action<T> action) where T : class
            => action(@object);
    }
}
