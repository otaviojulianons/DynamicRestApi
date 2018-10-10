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

namespace Application.Services
{
    public class DynamicAppService : 
        INotificationHandler<GenerateDynamicObjectsEvent>,
        INotificationHandler<AfterInsertEntityEvent>,
        INotificationHandler<AfterDeleteEntityEvent>
    {
        private IDynamicService _serviceDynamic;
        private IRepository<EntityDomain> _entityRepository;
        private IRepository<LanguageDomain> _languageRepository;
        private IServiceProvider _serviceProvider;
        private IJsonRepository _jsonRepository;

        public DynamicAppService(
            IDynamicService serviceDynamic,
            IServiceProvider serviceProvider,
            IRepository<EntityDomain> entityRepository,
            IRepository<LanguageDomain> languageRepository,
            IJsonRepository jsonRepository
            )
        {
            _serviceDynamic = serviceDynamic;
            _entityRepository = entityRepository;
            _languageRepository = languageRepository;
            _serviceProvider = serviceProvider;
            _jsonRepository = jsonRepository;
        }

        public async Task<string> GetSwaggerJson() => await _jsonRepository.Get();


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
            _jsonRepository.Update(json);
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
