using Domain.Entities.EntityAggregate;
using Domain.Interfaces.Events;

namespace Domain.Events
{
    public class AfterDeleteEntityEvent : IAfterDeleteDomainEvent<EntityDomain>
    {
        public AfterDeleteEntityEvent(EntityDomain entity)
        {
            Entity = entity;
        }

        public EntityDomain Entity { get; private set; }
    }
}
