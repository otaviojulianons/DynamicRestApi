using Domain.Core.Interfaces.Events;
using Domain.Entities.EntityAggregate;

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
