using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Domain.Entities.LanguageAggregate;
using Domain.Events;
using Domain.Interfaces.Infrastructure;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.EventHandlers
{
    public class GenerateDynamicControllersEventHandler : INotificationHandler<GenerateDynamicControllersEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        private IDynamicService _serviceDynamic;
        private IRepository<EntityDomain> _entityRepository;
        private IRepository<LanguageDomain> _languageRepository;

        public GenerateDynamicControllersEventHandler(
            IServiceProvider serviceProvider,
            IDynamicService serviceDynamic,
            IRepository<EntityDomain> entityRepository,
            IRepository<LanguageDomain> languageRepository
            )
        {
            _serviceProvider = serviceProvider;
            _serviceDynamic = serviceDynamic;
            _entityRepository = entityRepository;
            _languageRepository = languageRepository;
        }


        public Task Handle(GenerateDynamicControllersEvent notification, CancellationToken cancellationToken)
        {
            var entities = _entityRepository.GetAll();
            var languageCharp = _languageRepository.GetById((long)LanguageDomain.EnumLanguages.Csharp);

            var entitiesTemplates = new List<EntityTemplate>();

            if (notification.Entity == null)
                entitiesTemplates = entities.Select(entity => new EntityTemplate(entity, languageCharp)).ToList();
            else
                entitiesTemplates.Add(new EntityTemplate(notification.Entity, languageCharp));

            _serviceDynamic.GenerateControllerDynamic(_serviceProvider, entitiesTemplates.ToArray());

            return Task.CompletedTask;
        }


    }
}
