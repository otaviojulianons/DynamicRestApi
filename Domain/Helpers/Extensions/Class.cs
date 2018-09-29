﻿using System;

namespace Domain.Helpers.Extensions
{
    static class Class
    {
        public static void Do<T>(this T @object, Action<T> action) where T : class
        {
            if (@object != null)
                action(@object);
        }

        public static T When<T>(this T @object, Predicate<T> predicate) where T : class
        {
            return predicate(@object) ? @object : null;
        }
    }
}
