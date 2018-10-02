using Domain.Interfaces.Structure;
using MediatR;

namespace Domain.Interfaces.Events
{
    public interface IAfterDeleteDomainEvent<TEntity> : INotification where TEntity : IEntity
    {
        TEntity Entity { get; }
    }
}
