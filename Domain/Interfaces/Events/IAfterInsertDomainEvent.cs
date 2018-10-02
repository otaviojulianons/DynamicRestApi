using Domain.Interfaces.Structure;
using MediatR;

namespace Domain.Interfaces.Events
{
    public interface IAfterInsertDomainEvent<TEntity> : INotification where TEntity : IEntity
    {
        TEntity Entity { get; }
    }
}
