using Application.Commands;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Domain.Interfaces.Infrastructure;
using Infrastructure.DataTypes.Factories;
using Infrastructure.Templates;
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
        private IDocumentationRepository _swaggerRepository;

        public CreateDynamicDocumentationCommandHandler(
            IDynamicService serviceDynamic,
            IServiceProvider serviceProvider,
            IRepository<EntityDomain> entityRepository,
            IDocumentationRepository swaggerRepository
            )
        {
            _serviceDynamic = serviceDynamic;
            _entityRepository = entityRepository;
            _swaggerRepository = swaggerRepository;
        }

        public Task<bool> Handle(CreateDynamicDocumentationCommand request, CancellationToken cancellationToken)
        {
           
            var entities = _entityRepository.GetAll();
            var json = _serviceDynamic.GenerateSwaggerJsonFile(entities);
            _swaggerRepository.Update(json);
            return Task.FromResult(true);
        }
    }
}
