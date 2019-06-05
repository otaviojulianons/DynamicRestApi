using Application.Commands;
using Domain.Core.Implementation.Events;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Domain.Interfaces.Infrastructure;
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

        public AfterDeleteEntityEventHandler(
            IDatabaseService databaseService,
            IMediator mediator,
            IRepository<EntityDomain> entityRepository
            )
        {
            _databaseService = databaseService;
            _mediator = mediator;
            _entityRepository = entityRepository;
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
