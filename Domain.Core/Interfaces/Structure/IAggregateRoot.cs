using MediatR;
using System.Collections.Generic;

namespace Domain.Core.Interfaces.Structure
{
    public interface IAggregateRoot : IEntity
    {
        IList<INotification> Notifications { get; }
    }
}
