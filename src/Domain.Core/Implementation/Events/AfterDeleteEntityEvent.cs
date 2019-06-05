using Domain.Core.Interfaces.Events;
using Domain.Core.Interfaces.Structure;

namespace Domain.Core.Implementation.Events
{
    public class AfterDeleteEntityEvent<TEntity> : IAfterDeleteDomainEvent<TEntity> where TEntity : IEntity
    {
        public AfterDeleteEntityEvent(TEntity entity)
        {
            Entity = entity;
        }

        public TEntity Entity { get; private set; }
    }
}
