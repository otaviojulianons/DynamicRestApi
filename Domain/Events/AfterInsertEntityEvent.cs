using Domain.Entities.EntityAggregate;
using Domain.Interfaces.Domain;

namespace Domain.Events
{
    public class AfterInsertEntityEvent : IAfterInsertDomainEvent<EntityDomain>
    {
        public AfterInsertEntityEvent(EntityDomain entity)
        {
            Entity = entity;
        }

        public EntityDomain Entity { get; private set; }
    }
}
