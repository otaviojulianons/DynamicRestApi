using Application.Commands;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Domain.Entities.LanguageAggregate;
using Domain.Interfaces.Infrastructure;
using Domain.Models;
using Infrastructure.DataTypes.CSharp;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class CreateDynamicEndpointCommandHandler : IRequestHandler<CreateDynamicEndpointCommand, bool>
    {
        private readonly IServiceProvider _serviceProvider;
        private IDynamicService _serviceDynamic;
        private IMediator _mediator;
        private IRepository<EntityDomain> _entityRepository;
        private IRepository<LanguageDomain> _languageRepository;

        public CreateDynamicEndpointCommandHandler(
            IServiceProvider serviceProvider,
            IDynamicService serviceDynamic,
            IMediator mediator,
            IRepository<LanguageDomain> languageRepository
            )
        {
            _serviceProvider = serviceProvider;
            _serviceDynamic = serviceDynamic;
            _mediator = mediator;
            _languageRepository = languageRepository;
        }

        public Task<bool> Handle(CreateDynamicEndpointCommand request, CancellationToken cancellationToken)
        {
            var factoryCSharpDataType = new CSharpDataTypeFactory();
            var entitiesTemplates = 
                request.Entities.Select(entity => new EntityTemplate(entity, factoryCSharpDataType));

            _serviceDynamic.GenerateControllerDynamic(_serviceProvider, entitiesTemplates.ToArray());
            // return _mediator.Send(new CreateDynamicDocumentationCommand());
            return Task.FromResult(true);
        }

 
    }
}
