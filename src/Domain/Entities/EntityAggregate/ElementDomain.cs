using Domain.ValueObjects;

namespace Domain.Entities.EntityAggregate
{
    public class ElementDomain
    {

        public ElementDomain(EntityDomain entity, EnumDataTypes dataType)
        {
            Entity = entity;
            DataType = dataType;
        }

        public EntityDomain Entity { get; set; }
        public EnumDataTypes DataType { get; set; }
    }
}
