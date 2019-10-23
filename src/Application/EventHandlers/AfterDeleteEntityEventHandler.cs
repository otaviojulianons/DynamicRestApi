using Application.Commands;
using Domain.Core.Implementation.Events;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Domain.Interfaces.Infrastructure;
using Infrastructure.CrossCutting.WebSockets;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.EventHandlers
{
    public class AfterDeleteEntityEventHandler : INotificationHandler<AfterDeleteEntityEvent<EntityDomain>>
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMediator _mediator;
        private readonly IRepository<EntityDomain> _entityRepository;
        private readonly WebSocketService _webSocketManager;

        public AfterDeleteEntityEventHandler(
            IDatabaseService databaseService,
            IMediator mediator,
            IRepository<EntityDomain> entityRepository,
            WebSocketService webSocketManager
            )
        {
            _databaseService = databaseService;
            _mediator = mediator;
            _entityRepository = entityRepository;
            _webSocketManager = webSocketManager;
        }

        public Task Handle(AfterDeleteEntityEvent<EntityDomain> notification, CancellationToken cancellationToken)
        {
            var entities = _entityRepository.GetAll();
            _databaseService.DropEntity(notification.Entity.Name);
            _mediator.Send(new CreateDynamicEndpointCommand(entities));
            return Task.CompletedTask;
        }

    }
}
