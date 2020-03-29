using System;
using Domain.Core.Interfaces.Structure;
using GraphQL.Types;

namespace Infrastructure.GraphQL
{
    public class EntityType<TGenericEntity, TId> : ObjectGraphType<TGenericEntity>
        where TGenericEntity : IGenericEntity<TId>
    {
        public EntityType()
        {
            var type = typeof(TGenericEntity);
            Name = type.Name;
            foreach (var property in type.GetProperties())
            {
                var propertyType = GetPropertyType(property.PropertyType);
                if(propertyType != null)
                    Field(propertyType, property.Name);
            }
        }

        private Type GetPropertyType(Type type)
        {
            if (type == typeof(Guid))
                return typeof(IdGraphType);
            if (type == typeof(bool))
                return typeof(BooleanGraphType);
            if (type == typeof(string))
                return typeof(StringGraphType);
            if (type == typeof(int))
                return typeof(IntGraphType);
            if (type == typeof(decimal))
                return typeof(FloatGraphType);
            if (type == typeof(long))
                return typeof(IntGraphType);
            if (type == typeof(DateTime))
                return typeof(DateTimeGraphType);
            return null;
        }
    }
}