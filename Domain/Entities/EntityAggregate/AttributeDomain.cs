using Domain.Core.Interfaces.Structure;
using Domain.Core.ValueObjects;
using FluentValidation;
using System;

namespace Domain.Entities.EntityAggregate
{
    public class AttributeDomain : IEntity ,ISelfValidation<AttributeDomain>
    {

        public AttributeDomain(Name name, bool allowNull, int? length, string genericType)
            :this(name,allowNull,length,genericType,null)
        {
        }

        public AttributeDomain(Name name, bool allowNull, int? length,string genericType, DataTypeDomain dataType)
        {
            Name = name;
            AllowNull = allowNull;
            Length = length;
            DataType = dataType;
            GenericType = genericType;
        }

        public IValidator<AttributeDomain> Validator => new AttributeValidator();

        public Guid Id { get; private set; }

        public Name Name { get; private set; }

        public string GenericType { get; private set; }

        public bool HasGenericType => !string.IsNullOrEmpty(GenericType);

        public int? Length { get; private set; }

        public bool AllowNull { get; private set; }

        public DataTypeDomain DataType { get; private set; }

        public EntityDomain Entity { get; private set; }

        public bool IsIdentifier => 
            Name?.Value.ToLower() == "id" && (DataType?.Name)?.Value.ToLower() == "guid";


    }
}
