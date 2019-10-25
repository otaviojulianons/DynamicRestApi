using Domain.Core.Interfaces.Events;
using Domain.Core.Interfaces.Structure;
using MediatR;

namespace Domain.Core.Implementation.Events
{
    public class EntityInsertedDomaiEvent<TEntity> : INotification where TEntity : IEntity
    {
        public EntityInsertedDomaiEvent(TEntity entity)
        {
            Entity = entity;
        }

        public TEntity Entity { get; private set; }
    }
}
