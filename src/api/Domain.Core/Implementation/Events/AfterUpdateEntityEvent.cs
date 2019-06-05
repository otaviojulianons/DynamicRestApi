using Domain.Core.Interfaces.Events;
using Domain.Core.Interfaces.Structure;

namespace Domain.Core.Implementation.Events
{
    public class AfterUpdateEntityEvent<TEntity> : IAfterUpdateDomainEvent<TEntity> where TEntity : IEntity
    {
        public AfterUpdateEntityEvent(TEntity entity)
        {
            Entity = entity;
        }

        public TEntity Entity { get; private set; }
    }
}
