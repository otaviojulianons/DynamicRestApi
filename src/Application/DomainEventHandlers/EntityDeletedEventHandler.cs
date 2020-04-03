using Domain.Core.Implementation.Events;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DomainEventHandlers
{
    public class EntityDeletedEventHandler : INotificationHandler<EntityDeletedDomainEvent<EntityDomain>>
    {
        public Task Handle(EntityDeletedDomainEvent<EntityDomain> notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

    }
}
