using Domain.Core.Interfaces.Infrastructure;
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
    public class GenerateSwaggerFileEventHandler : INotificationHandler<GenerateSwaggerFileEvent>
    {
        private IDynamicService _serviceDynamic;
        private IRepository<EntityDomain> _entityRepository;
        private IRepository<LanguageDomain> _languageRepository;
        private ISwaggerRepository _swaggerRepository;

        public GenerateSwaggerFileEventHandler(
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
            _swaggerRepository = swaggerRepository;
        }

        public Task Handle(GenerateSwaggerFileEvent notification, CancellationToken cancellationToken)
        {
            var entities = _entityRepository.GetAll();
            var languageSwagger = _languageRepository.GetById((long)LanguageDomain.EnumLanguages.SwaggerDoc);
            var entitiesTemplates = entities.Select(entity => new EntityTemplate(entity, languageSwagger)).ToArray();

            var json = _serviceDynamic.GenerateSwaggerJsonFile(entitiesTemplates);
            _swaggerRepository.Update(json);
            return Task.CompletedTask;
        }
    }
}
