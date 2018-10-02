using MediatR;
using System.Collections.Generic;

namespace Domain.Interfaces.Structure
{
    public interface IAggregateRoot : IEntity
    {
        IList<INotification> Notifications { get; }
    }
}
