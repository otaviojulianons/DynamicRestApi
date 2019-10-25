using Application.Commands;
using Common.Notifications;
using Domain.Core.Implementation.Events;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class DeleteEntityCommandHandler : IRequestHandler<DeleteEntityCommand, bool>
    {
        private INotificationManager _notificationManager;
        private IMediator _mediator;
        private IRepository<EntityDomain> _entityRepository;

        public DeleteEntityCommandHandler(
            INotificationManager notificationManager,
            IMediator mediator,
            IRepository<EntityDomain> entityRepository
            )
        {
            _notificationManager = notificationManager;
            _mediator = mediator;
            _entityRepository = entityRepository;
        }

        public async Task<bool> Handle(DeleteEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = _entityRepository.GetById(request.Id);
            if(entity == null)
            {
                _notificationManager.Errors.Add(new NotificationMessage("Entity not found."));
                return false;
            }

            _entityRepository.Delete(request.Id);
            await _mediator.Publish(new EntityDeletedDomainEvent<EntityDomain>(entity));
            return true;
        }
    }
}
