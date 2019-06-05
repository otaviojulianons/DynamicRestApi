using System;
using System.Linq.Expressions;

namespace Infrastructure.CrossCutting.Extensions
{
    public static class Objects
    {
        public static TObject SetProperty<TObject,TValue>(this TObject @object, Expression<Func<TObject, TValue>> expression, TValue value)
        {
            var memberExpression = (MemberExpression)expression.Body;
            var propertyName = memberExpression.Member.Name;

            return @object.SetProperty(propertyName,value);
        }

        public static TObject SetProperty<TObject, TValue>(this TObject @object, string propertyName, TValue value)
        {
            var property = typeof(TObject).GetProperty(propertyName);
            property.SetValue(@object, value);
            return @object;
        }
    }
}
