using Domain.Core.ValueObjects;
using Domain.ValueObjects;

namespace Domain.Entities.EntityAggregate
{
    public class ElementDomain
    {

        public ElementDomain(EntityDomain entity, EnumElementType type)
        {
            Entity = entity;
            Type = type;
        }

        public EntityDomain Entity { get; set; }
        public EnumElementType Type { get; set; }
    }
}
