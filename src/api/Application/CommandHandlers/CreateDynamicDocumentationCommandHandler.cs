using Application.Commands;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Domain.Entities.LanguageAggregate;
using Domain.Interfaces.Infrastructure;
using Domain.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class CreateDynamicDocumentationCommandHandler : IRequestHandler<CreateDynamicDocumentationCommand, bool>
    {
        private IDynamicService _serviceDynamic;
        private IRepository<EntityDomain> _entityRepository;
        private IRepository<LanguageDomain> _languageRepository;
        private IDocumentationRepository _swaggerRepository;

        public CreateDynamicDocumentationCommandHandler(
            IDynamicService serviceDynamic,
            IServiceProvider serviceProvider,
            IRepository<EntityDomain> entityRepository,
            IRepository<LanguageDomain> languageRepository,
            IDocumentationRepository swaggerRepository
            )
        {
            _serviceDynamic = serviceDynamic;
            _entityRepository = entityRepository;
            _languageRepository = languageRepository;
            _swaggerRepository = swaggerRepository;
        }

        public Task<bool> Handle(CreateDynamicDocumentationCommand request, CancellationToken cancellationToken)
        {
            var entities = _entityRepository.GetAll();
            var languageSwagger = _languageRepository.GetById(LanguageDomain.LANGUAGE_SWAGGER);
            var entitiesTemplates = entities.Select(entity => new EntityTemplate(entity, languageSwagger)).ToArray();

            var json = _serviceDynamic.GenerateSwaggerJsonFile(entitiesTemplates);
            _swaggerRepository.Update(json);
            return Task.FromResult(true);
        }
    }
}
