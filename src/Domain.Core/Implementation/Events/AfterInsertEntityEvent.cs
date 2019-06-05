using Domain.Core.Interfaces.Events;
using Domain.Core.Interfaces.Structure;

namespace Domain.Core.Implementation.Events
{
    public class AfterInsertEntityEvent<TEntity> : IAfterInsertDomainEvent<TEntity> where TEntity : IEntity
    {
        public AfterInsertEntityEvent(TEntity entity)
        {
            Entity = entity;
        }

        public TEntity Entity { get; private set; }
    }
}
