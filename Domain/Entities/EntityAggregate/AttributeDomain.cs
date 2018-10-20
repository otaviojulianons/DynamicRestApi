using Domain.Core.Interfaces.Structure;
using Domain.Core.ValueObjects;
using FluentValidation;

namespace Domain.Entities.EntityAggregate
{
    public class AttributeDomain : IEntity ,ISelfValidation<AttributeDomain>
    {

        public AttributeDomain(Name name, bool allowNull, int? length)
            :this(name,allowNull,length,null)
        {
        }

        public AttributeDomain(Name name, bool allowNull, int? length, DataTypeDomain dataType)
        {
            Name = name;
            AllowNull = allowNull;
            Length = length;
            DataType = dataType;
        }

        public IValidator<AttributeDomain> Validator => new AttributeValidator();

        public long Id { get; private set; }

        public Name Name { get; private set; }

        public int? Length { get; private set; }

        public bool AllowNull { get; private set; }

        public DataTypeDomain DataType { get; private set; }

        public EntityDomain Entity { get; private set; }

        public bool IsIdentifier => 
            Name?.Value.ToLower() == "id" && (DataType?.Name)?.Value.ToLower() == "long";


    }
}
