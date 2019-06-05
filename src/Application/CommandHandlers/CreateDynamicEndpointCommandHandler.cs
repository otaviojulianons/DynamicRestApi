using Application.Commands;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Domain.Interfaces.Infrastructure;
using Infrastructure.DataTypes.CSharp;
using Infrastructure.DataTypes.Factories;
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

        public CreateDynamicEndpointCommandHandler(
            IServiceProvider serviceProvider,
            IDynamicService serviceDynamic,
            IMediator mediator
            )
        {
            _serviceProvider = serviceProvider;
            _serviceDynamic = serviceDynamic;
            _mediator = mediator;
        }

        public Task<bool> Handle(CreateDynamicEndpointCommand request, CancellationToken cancellationToken)
        {
            // var factoryCSharpDataType = new CSharpDataTypeFactory();
            // var entitiesTemplates = 
            //     request.Entities.Select(entity => new EntityTemplate(entity, factoryCSharpDataType));

            // _serviceDynamic.GenerateControllerDynamic(_serviceProvider, entitiesTemplates.ToArray());
            // return _mediator.Send(new CreateDynamicDocumentationCommand());
            return Task.FromResult(true);
        }

 
    }
}
