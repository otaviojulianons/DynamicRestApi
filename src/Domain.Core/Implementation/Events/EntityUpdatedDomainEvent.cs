using Domain.Core.Interfaces.Events;
using Domain.Core.Interfaces.Structure;

namespace Domain.Core.Implementation.Events
{
    public class EntityUpdatedDomainEvent<TEntity> : IEntityUpdatedDomainEvent<TEntity> where TEntity : IEntity
    {
        public EntityUpdatedDomainEvent(TEntity entity)
        {
            Entity = entity;
        }

        public TEntity Entity { get; private set; }
    }
}
