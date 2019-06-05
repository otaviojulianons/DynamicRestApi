using Application.Commands;
using Domain.Core.Implementation.Events;
using Domain.Entities.EntityAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.EventHandlers
{
    public class AfterInsertEntityEventHandler :  INotificationHandler<AfterInsertEntityEvent<EntityDomain>>
    {

        private readonly IMediator _mediator;

        public AfterInsertEntityEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Handle(AfterInsertEntityEvent<EntityDomain> notification, CancellationToken cancellationToken)
        {
            _mediator.Send(new CreateDynamicEndpointCommand(notification.Entity));
            return Task.CompletedTask;
        }

    }
}
