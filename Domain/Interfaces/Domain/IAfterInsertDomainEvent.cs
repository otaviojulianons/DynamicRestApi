using Domain.Interfaces.Structure;
using MediatR;

namespace Domain.Interfaces.Domain
{
    public interface IAfterInsertDomainEvent<TEntity> : INotification where TEntity : IEntity
    {
        TEntity Entity { get; }
    }
}
