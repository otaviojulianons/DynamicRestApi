using System;
using System.Collections.Generic;

namespace Common.Extensions
{
    public static class Array
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list ?? new List<T>()) 
                action(item);
        }

    }

}
