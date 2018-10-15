using Domain.Core.Interfaces.Structure;
using Domain.Core.Interfaces.Structure;
using FluentValidation;

namespace Domain.Entities.EntityAggregate
{
    public class AttributeDomain : IEntity ,ISelfValidation<AttributeDomain>
    {

        public AttributeDomain(string name, bool allowNull, int? length)
            :this(name,allowNull,length,null)
        {
        }

        public AttributeDomain(string name, bool allowNull, int? length, DataTypeDomain dataType)
        {
            Name = name;
            AllowNull = allowNull;
            Length = length;
            DataType = dataType;
        }

        public IValidator<AttributeDomain> Validator => new AttributeValidator();

        public long Id { get; private set; }

        public string Name { get; private set; }

        public int? Length { get; private set; }

        public bool AllowNull { get; private set; }

        public DataTypeDomain DataType { get; private set; }

        public EntityDomain Entity { get; private set; }

        public bool IsIdentifier => 
            Name?.ToLower() == "id" && (DataType?.Name)?.ToLower() == "long";


    }
}
