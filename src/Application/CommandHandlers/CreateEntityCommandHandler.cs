using Application.Commands;
using AutoMapper;
using Common.Extensions;
using Common.Notifications;
using Domain.Core.Implementation.Events;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Core.ValueObjects;
using Domain.Entities.EntityAggregate;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class CreateEntityCommandHandler : IRequestHandler<CreateEntityCommand, bool>
    {
        private INotificationManager _notificationManager;
        private IMediator _mediator;
        private IRepository<EntityDomain> _entityRepository;

        public CreateEntityCommandHandler(
            INotificationManager notificationManager,
            IMediator mediator,
            IRepository<EntityDomain> entityRepository
            )
        {
            _notificationManager = notificationManager;
            _mediator = mediator;
            _entityRepository = entityRepository;
        }

        public async Task<bool> Handle(CreateEntityCommand request, CancellationToken cancellationToken)
        {
            var entityDomain = Mapper.Map<EntityDomain>(request);
            request.Attributes.ForEach(attribute =>
            {
                var attributeDomain = new AttributeDomain(
                    new Name(attribute.Name),
                    attribute.DataType,
                    attribute.AllowNull,
                    attribute.Length
                );
                
                entityDomain.AddAttribute(attributeDomain);
            });

            if (!entityDomain.IsValid(_notificationManager))
                return false;

            _entityRepository.Insert(entityDomain);
            return true;
        }

 
    }
}
