using Domain.Interfaces.Structure;
using Domain.ValueObjects;
using FluentValidation;

namespace Domain.Entities.EntityAggregate
{
    public class AttributeDomain : ISelfValidation<AttributeDomain>
    {

        public IValidator<AttributeDomain> Validator => new AttributeValidator();

        public long Id { get; set; }

        public long EntityId { get; set; }

        public long DataTypeId { get; set; }

        public string Name { get; set; }

        public int? Length { get; set; }

        public bool AllowNull { get; set; }

        public string DataTypeName { get; set; }

        public AttributeTypeLanguage TypeLanguage { get; set; }

        public DataTypeDomain DataType { get; set; }

        public EntityDomain Entity { get; set; }

        public bool IsIdentifier => Name?.ToLower() == "id" && DataTypeName?.ToLower() == "long";


    }
}
