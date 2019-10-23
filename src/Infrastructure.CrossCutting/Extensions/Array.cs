using System;
using System.Collections.Generic;

namespace Infrastructure.CrossCutting.Extensions
{
    public static class Array
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list) 
                action(item);
        }

    }

}
