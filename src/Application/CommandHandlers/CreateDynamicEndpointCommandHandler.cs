using Application.Commands;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Domain.Interfaces.Infrastructure;
using InfrastructureTypes.CSharp;
using InfrastructureTypes.Factories;
using Infrastructure.Templates;
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
            _serviceDynamic.GenerateControllerDynamic(_serviceProvider, request.Entities);
            return _mediator.Send(new CreateDynamicDocumentationCommand());
        }

 
    }
}
