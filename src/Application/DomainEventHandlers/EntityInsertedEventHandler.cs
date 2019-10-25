﻿using Domain.Core.Implementation.Events;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DomainEventHandlers
{
    public class EntityInsertedEventHandler :  INotificationHandler<EntityInsertedDomaiEvent<EntityDomain>>
    {
        private readonly IRepository<EntityDomain> _entityRepository;
        private readonly IDynamicDomainService _dynamicService;
        private readonly IDocumentationDomainService _documentationService;

        public EntityInsertedEventHandler(
            IRepository<EntityDomain> entityRepository,
            IDynamicDomainService dynamicService,
            IDocumentationDomainService documentationService)
        {
            _entityRepository = entityRepository;
            _dynamicService = dynamicService;
            _documentationService = documentationService;
        }

        public Task Handle(EntityInsertedDomaiEvent<EntityDomain> notification, CancellationToken cancellationToken)
        {
            _dynamicService.GenerateType(notification.Entity);

            var entities = _entityRepository.GetAll();
            _documentationService.GenerateDocumentation(entities);

            return Task.CompletedTask;
        }

    }
}
