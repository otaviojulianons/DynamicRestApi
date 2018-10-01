using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Commands;
using Domain.Entities.EntityAggregate;
using Domain.Entities.LanguageAggregate;
using Domain.Interfaces.Infrastructure;
using Domain.Interfaces.Structure;
using MediatR;

namespace Application.Services
{
    public class DynamicAppService : 
        INotificationHandler<GenerateDynamicDocumentationCommand>,
        INotificationHandler<GenerateDynamicControllerCommand>
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

        public Task Handle(GenerateDynamicDocumentationCommand notification, CancellationToken cancellationToken)
        {
            var entities = _entityRepository.GetAll().ToArray();
            var languageSwagger = _languageRepository.GetById((long)LanguageDomain.EnumLanguages.SwaggerDoc);
            _serviceDynamic.GenerateSwaggerJsonFile(languageSwagger,entities);
            return Task.CompletedTask;
        }

        public Task Handle(GenerateDynamicControllerCommand notification, CancellationToken cancellationToken)
        {
            EntityDomain[] entities = notification.Entity == null
                                        ? _entityRepository.GetAll().ToArray()
                                        : new EntityDomain[] { notification.Entity };
            var languageCharp = _languageRepository.GetById((long)LanguageDomain.EnumLanguages.Csharp);
            _serviceDynamic.GenerateControllerDynamic(_serviceProvider,languageCharp, entities);
            return Task.CompletedTask;
        }
    }
}
