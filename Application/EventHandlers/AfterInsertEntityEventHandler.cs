using Application.Commands;
using Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.EventHandlers
{
    public class AfterInsertEntityEventHandler :  INotificationHandler<AfterInsertEntityEvent>
    {

        private readonly IMediator _mediator;

        public AfterInsertEntityEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Handle(AfterInsertEntityEvent notification, CancellationToken cancellationToken)
        {
            _mediator.Send(new CreateDynamicEndpointCommand(notification.Entity));
            return Task.CompletedTask;
        }

    }
}
