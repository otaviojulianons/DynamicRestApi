using Domain.Events;
using Domain.Interfaces.Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.EventHandlers
{
    public class EntityEventHandler : 
        INotificationHandler<AfterInsertEntityEvent>,
        INotificationHandler<AfterDeleteEntityEvent>
    {
        private IDatabaseService _databaseService;
        private readonly IMediator _mediator;

        public EntityEventHandler(
            IDatabaseService databaseService,
            IMediator mediator
            )
        {
            _databaseService = databaseService;
            _mediator = mediator;
        }

        public Task Handle(AfterInsertEntityEvent notification, CancellationToken cancellationToken)
        {
            _mediator.Publish(new GenerateDynamicControllersEvent(notification.Entity));
            return Task.CompletedTask;
        }

        public Task Handle(AfterDeleteEntityEvent notification, CancellationToken cancellationToken)
        {
            _databaseService.DropEntity(notification.Entity.Name);
            _mediator.Publish(new GenerateDynamicControllersEvent());
            return Task.CompletedTask;
        }

    }
}
