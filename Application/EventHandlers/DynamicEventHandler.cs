﻿using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Domain.Entities.LanguageAggregate;
using Domain.Events;
using Domain.Interfaces.Infrastructure;
using Domain.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.EventHandlers
{
    public class DynamicEventHandler : 
        INotificationHandler<GenerateDynamicObjectsEvent>,
        INotificationHandler<AfterInsertEntityEvent>,
        INotificationHandler<AfterDeleteEntityEvent>
    {
        private IDynamicService _serviceDynamic;
        private IRepository<EntityDomain> _entityRepository;
        private IRepository<LanguageDomain> _languageRepository;
        private IServiceProvider _serviceProvider;
        private IDatabaseService _databaseService;
        private ISwaggerRepository _swaggerRepository;

        public DynamicEventHandler(
            IDynamicService serviceDynamic,
            IServiceProvider serviceProvider,
            IRepository<EntityDomain> entityRepository,
            IRepository<LanguageDomain> languageRepository,
            IDatabaseService databaseService,
            ISwaggerRepository swaggerRepository
            )
        {
            _serviceDynamic = serviceDynamic;
            _entityRepository = entityRepository;
            _languageRepository = languageRepository;
            _serviceProvider = serviceProvider;
            _databaseService = databaseService;
            _swaggerRepository = swaggerRepository;
        }

        public Task Handle(GenerateDynamicObjectsEvent notification, CancellationToken cancellationToken)
        {
            GenerateDynamicControllers();
            GenerateSwaggerJsonFile();
            return Task.CompletedTask;
        }

        public Task Handle(AfterInsertEntityEvent notification, CancellationToken cancellationToken)
        {
            CreateDynamicController(notification.Entity);
            GenerateSwaggerJsonFile();
            return Task.CompletedTask;
        }

        public Task Handle(AfterDeleteEntityEvent notification, CancellationToken cancellationToken)
        {
            _databaseService.DropEntity(notification.Entity.Name);
            GenerateDynamicControllers();
            GenerateSwaggerJsonFile();
            return Task.CompletedTask;
        }

        private void GenerateSwaggerJsonFile()
        {
            var entities = _entityRepository.GetAll();
            var languageSwagger = _languageRepository.GetById((long)LanguageDomain.EnumLanguages.SwaggerDoc);
            var entitiesTemplates = entities.Select(entity => new EntityTemplate(entity, languageSwagger)).ToArray();

            var json = _serviceDynamic.GenerateSwaggerJsonFile(entitiesTemplates);
            _swaggerRepository.Update(json);
        }

        private void GenerateDynamicControllers()
        {
            var entities = _entityRepository.GetAll();
            var languageCharp = _languageRepository.GetById((long)LanguageDomain.EnumLanguages.Csharp);
            var entitiesTemplates = entities.Select(entity => new EntityTemplate(entity, languageCharp)).ToArray();

            _serviceDynamic.GenerateControllerDynamic(_serviceProvider, entitiesTemplates);
        }

        private void CreateDynamicController(EntityDomain entity)
        {
            var languageCharp = _languageRepository.GetById((long)LanguageDomain.EnumLanguages.Csharp);
            var entityTemplate = new EntityTemplate(entity, languageCharp);

            _serviceDynamic.GenerateControllerDynamic(_serviceProvider, entityTemplate);
        }

    }
}
