using Domain.Entities.EntityAggregate;
using Domain.Entities.LanguageAggregate;
using Domain.Events;
using Domain.Interfaces.Infrastructure;
using Domain.Interfaces.Structure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DynamicAppService : 
        INotificationHandler<GenerateDynamicDocumentationEvent>,
        INotificationHandler<GenerateDynamicControllersEvent>,
        INotificationHandler<CreateDynamicControllerEvent>,
        INotificationHandler<AfterInsertEntityEvent>
    {
        private IDynamicService _serviceDynamic;
        private IRepository<EntityDomain> _entityRepository;
        private IRepository<LanguageDomain> _languageRepository;
        private IServiceProvider _serviceProvider;

        public DynamicAppService(
            IDynamicService serviceDynamic,
            IServiceProvider serviceProvider,
            IRepository<EntityDomain> entityRepository,
            IRepository<LanguageDomain> languageRepository
            )
        {
            _serviceDynamic = serviceDynamic;
            _entityRepository = entityRepository;
            _languageRepository = languageRepository;
            _serviceProvider = serviceProvider;
        }


        public void GenerateSwaggerJsonFile()
        {
            var entities = _entityRepository.GetAll().ToArray();
            var languageSwagger = _languageRepository.GetById((long)LanguageDomain.EnumLanguages.SwaggerDoc);
            _serviceDynamic.GenerateSwaggerJsonFile(languageSwagger, entities);
        }

        public void GenerateDynamicControllers()
        {
            EntityDomain[] entities = _entityRepository.GetAll().ToArray();
            var languageCharp = _languageRepository.GetById((long)LanguageDomain.EnumLanguages.Csharp);
            _serviceDynamic.GenerateControllerDynamic(_serviceProvider, languageCharp, entities);
        }

        public void CreateDynamicController(EntityDomain entity)
        {
            var languageCharp = _languageRepository.GetById((long)LanguageDomain.EnumLanguages.Csharp);
            _serviceDynamic.GenerateControllerDynamic(_serviceProvider, languageCharp, entity);
        }

        public Task Handle(GenerateDynamicDocumentationEvent notification, CancellationToken cancellationToken)
        {
            GenerateSwaggerJsonFile();
            return Task.CompletedTask;
        }

        public Task Handle(GenerateDynamicControllersEvent notification, CancellationToken cancellationToken)
        {
            GenerateDynamicControllers();
            return Task.CompletedTask;
        }

        public Task Handle(CreateDynamicControllerEvent notification, CancellationToken cancellationToken)
        {
            CreateDynamicController(notification.Entity);
            return Task.CompletedTask;
        }

        public Task Handle(AfterInsertEntityEvent notification, CancellationToken cancellationToken)
        {
            CreateDynamicController(notification.Entity);
            GenerateSwaggerJsonFile();
            return Task.CompletedTask;
        }
    }
}
