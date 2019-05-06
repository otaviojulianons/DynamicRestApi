using Domain.Core.Interfaces.Events;
using Domain.Entities.EntityAggregate;

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
