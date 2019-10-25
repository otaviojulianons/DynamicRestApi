using Domain.Core.Interfaces.Events;
using Domain.Core.Interfaces.Structure;

namespace Domain.Core.Implementation.Events
{
    public class EntityDeletedDomainEvent<TEntity> : IEntityDeletedDomainEvent<TEntity> where TEntity : IEntity
    {
        public EntityDeletedDomainEvent(TEntity entity)
        {
            Entity = entity;
        }

        public TEntity Entity { get; private set; }
    }
}
