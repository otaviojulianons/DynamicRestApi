using System;
using System.Linq;

namespace Common.Extensions
{
    public static class Types
    {
        public static bool ImplementsGericType(this Type type, Type @base) =>
            type.GetInterfaces().Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == @base);

        public static bool InheritsGericType(this Type type, Type @base) =>
            type.BaseType.IsGenericType && type.GetGenericTypeDefinition() == @base;
    }
}
