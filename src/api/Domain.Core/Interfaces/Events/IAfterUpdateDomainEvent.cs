using Domain.Core.Interfaces.Structure;
using MediatR;

namespace Domain.Core.Interfaces.Events
{
    public interface IAfterUpdateDomainEvent<TEntity> : INotification where TEntity : IEntity
    {
        TEntity Entity { get; }
    }
}
